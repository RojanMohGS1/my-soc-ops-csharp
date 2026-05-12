using SocOps.Models;
using System.Text.Json;
using Microsoft.JSInterop;

namespace SocOps.Services;

public class BingoGameService
{
    private const string STORAGE_KEY = "bingo-game-state";
    private const int STORAGE_VERSION = 2;

    private readonly IJSRuntime _jsRuntime;

    public GameState CurrentGameState { get; private set; } = GameState.Start;
    public List<BingoSquareData> Board { get; private set; } = new();
    public BingoLine? WinningLine { get; private set; }
    public HashSet<int> WinningSquareIds => BingoLogicService.GetWinningSquareIds(WinningLine);
    public bool ShowBingoModal { get; private set; }

    // Gamified additions
    public int MarkedCount => Board?.Count(b => b.IsMarked) ?? 0;
    public double ProgressPercent => Board == null || Board.Count == 0 ? 0 : Math.Round((MarkedCount / (double)Board.Count) * 100, 1);
    public PlayerMetadata? Player { get; private set; }

    public event Action? OnStateChanged;

    public BingoGameService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        await LoadGameStateAsync();
    }

    public void StartGame()
    {
        Board = BingoLogicService.GenerateBoard();
        WinningLine = null;
        CurrentGameState = GameState.Playing;
        ShowBingoModal = false;
        _ = SaveGameStateAsync(); // Fire and forget
        NotifyStateChanged();
    }

    public void HandleSquareClick(int squareId)
    {
        Board = BingoLogicService.ToggleSquare(Board, squareId);

        // Check for bingo after toggling
        if (WinningLine == null)
        {
            var bingo = BingoLogicService.CheckBingo(Board);
            if (bingo != null)
            {
                WinningLine = bingo;
                CurrentGameState = GameState.Bingo;
                ShowBingoModal = true;
                // trigger confetti via JS interop (fire-and-forget)
                _ = _jsRuntime.InvokeVoidAsync("confetti.start");
            }
        }

        _ = SaveGameStateAsync(); // Fire and forget
        NotifyStateChanged();
    }

    public void ResetGame()
    {
        CurrentGameState = GameState.Start;
        Board = new();
        WinningLine = null;
        ShowBingoModal = false;
        _ = SaveGameStateAsync(); // Fire and forget
        NotifyStateChanged();
    }

    public void DismissModal()
    {
        ShowBingoModal = false;
        NotifyStateChanged();
    }

    public async Task SetPlayerAsync(PlayerMetadata? player)
    {
        Player = player;
        await SaveGameStateAsync();
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChanged?.Invoke();

    private async Task LoadGameStateAsync()
    {
        try
        {
            var saved = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", STORAGE_KEY);
            if (!string.IsNullOrEmpty(saved))
            {
                var data = JsonSerializer.Deserialize<StoredGameData>(saved);
                if (data != null)
                {
                    // Accept older versions when possible, but persist with current version
                    CurrentGameState = data.GameState;
                    Board = data.Board ?? new List<BingoSquareData>();
                    WinningLine = data.WinningLine;
                    Player = data.Player;
                    ShowBingoModal = data.ShowBingoModal;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load game state: {ex.Message}");
        }
    }

    private async Task SaveGameStateAsync()
    {
        try
        {
            var data = new StoredGameData
            {
                Version = STORAGE_VERSION,
                GameState = CurrentGameState,
                Board = Board,
                WinningLine = WinningLine,
                Player = Player,
                ShowBingoModal = ShowBingoModal
            };
            var json = JsonSerializer.Serialize(data);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", STORAGE_KEY, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save game state: {ex.Message}");
        }
    }

    private class StoredGameData
    {
        public int Version { get; set; }
        public GameState GameState { get; set; }
        public List<BingoSquareData> Board { get; set; } = new();
        public BingoLine? WinningLine { get; set; }
        public PlayerMetadata? Player { get; set; }
        public bool ShowBingoModal { get; set; }
    }
}

🌐 [Português (BR)](README.pt_BR.md) | [Español](README.es.md)

# Soc Ops — Social Bingo (Blazor)

Playful, workshop-ready Social Bingo built with Blazor WebAssembly (.NET 10).

🎮 Play: https://dotnet-presentations.github.io/vscode-github-copilot-agent-lab/  •  📚 Lab Guide: https://dotnet-presentations.github.io/vscode-github-copilot-agent-lab/docs/

---

## Why this repo

- Great as a hands-on demo for Blazor, WebAssembly, and event-driven UI.
- Includes workshop guides and agent examples for learning multi-agent workflows.
- Small, readable codebase — ideal for teaching or prototyping UI logic.

## Quick Start

```bash
# Run dev server
cd SocOps
dotnet run
# Open http://localhost:5166
```

## What you'll find

- `SocOps/Components` — Reusable Blazor components (board, squares, modal)
- `SocOps/Services` — Game state & logic (`BingoGameService`, `BingoLogicService`)
- `workshop/` — Step-by-step lab exercises and agent examples
- `docs/` — Static guide and localized content

## Features

- Bingo-style quiz flow with local state persistence
- Minimal, framework-focused UI for easy modification
- Example agent manifests in `.solutions/*/.github/agents/`

## Development checklist (recommended)

- Lint: `dotnet format --verify-no-changes` (or run `dotnet format`)
- Build: `dotnet build SocOps/SocOps.csproj`
- Test: `dotnet test`

## Contributing

- Open issues and PRs welcome — keep changes small and focused.
- Follow C# naming conventions and run the checklist above before committing.

## License

MIT — see LICENSE


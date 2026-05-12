# Copilot Workspace Instructions

## Mandatory Development Checklist (must pass before commit)

- [ ] Lint: `dotnet format --verify-no-changes` (or run `dotnet format` to fix)
- [ ] Build: `dotnet build SocOps/SocOps.csproj`
- [ ] Test: `dotnet test`

## Quick Overview

Soc Ops is a small Blazor WebAssembly (.NET 10) Social Bingo app used for workshop exercises.

## Key Commands

- `dotnet format --verify-no-changes`  # lint/format check
- `dotnet build SocOps/SocOps.csproj`  # build
- `dotnet run --project SocOps`        # run dev server (http://localhost:5166)
- `dotnet test`                        # run tests

## Layout (short)

- `SocOps/Components` — UI components
- `SocOps/Services`   — game logic and state
- `SocOps/Pages`      — routable pages (Home.razor)
- `workshop/`         — guided exercises and agent examples

## Notes

- Keep changes minimal and follow C# naming conventions.
- See `.solutions/*/.github/agents/` for example agent manifests.


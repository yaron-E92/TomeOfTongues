# TomeOfTongues

TomeOfTongues is the technical name for the local-first, language-neutral
learning engine displayed as **Tome of Many Tongues**.

## Project structure

- `TomeOfTongues.Core` contains domain types and policies and has no project
  dependencies.
- `TomeOfTongues.Application` contains use cases and ports and depends only on
  Core.
- `TomeOfTongues.Content` contains generic `.totlang` contracts and tooling and
  depends only on Core.
- `TomeOfTongues.Content.Tool` is the generic content-tooling boundary and
  depends only on Content.
- `TomeOfTongues.Infrastructure` owns persistence and external adapters and
  depends on Core, Application, and Content.
- `tests/` mirrors the generic production projects and includes executable
  dependency-boundary tests.

Language projects are independent authoring projects that emit declarative
`.totlang` artifacts. Generic production and test projects must not reference a
`TomeOfTongues.Language.*` project, assembly, namespace, or type.

The MAUI host and optional SecondBrain adapter are intentionally outside the
non-GUI project set until their workload and external contracts are introduced.

## Verification

Install the .NET 10 SDK. The repository selects the 10.0.200 feature band and
accepts its latest servicing patch. Then run:

```powershell
dotnet restore TomeOfTongues.NonMaui.slnf
dotnet build TomeOfTongues.NonMaui.slnf --configuration Debug --no-restore
dotnet test TomeOfTongues.NonMaui.slnf --configuration Debug --no-build --no-restore
```

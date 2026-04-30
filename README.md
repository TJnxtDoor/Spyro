# Spyro

Prototype Unity-style gameplay scripts plus a .NET smoke runner.

## What Is Set Up

- `Spyro/Spyro.csproj` compiles the C# gameplay scripts into `Spyro.dll`.
- `Spyro.Runner/Spyro.Runner.csproj` is a small executable smoke check.
- `Spyro.sln` includes both projects.

## What Is Not Set Up Yet

This is not a complete Unity project yet. It is missing:

- `Assets/`
- `ProjectSettings/`
- `Packages/manifest.json`
- a `.unity` scene
- assigned prefabs, UI objects, audio clips, materials, and scene references

Because of that, `dotnet run` can verify the scripts compile, but it cannot play the actual game. The real game loop needs Unity.

## Quick Check

From this folder:

```powershell
dotnet build Spyro.sln
dotnet run --project Spyro.Runner/Spyro.Runner.csproj
```

Expected result:

- build succeeds
- runner prints that it found the compiled game library and key scripts

## Unity Setup Path

1. Install Unity Hub and a Unity 2021.3 LTS editor.
2. Create a new 3D Unity project.
3. Copy these script folders into that project's `Assets/Scripts/` folder:
   - `MainGame`
   - `Difficulty`
   - `Enemys`
   - `Skins`
   - `Worlds`
4. Create a test scene with a `GameManager` GameObject.
5. Add a player GameObject and attach `PlayerController` or `Player3DController`.
6. Assign inspector references for UI text, sliders, prefabs, audio clips, materials, and cameras.
7. Press Play in Unity.

## Current Tasks

- Finish character movement.
- Clean up `SkinManager`.
- Convert prototype achievement files into one real achievement system.
- Replace the temporary SDK/runner setup once this becomes a full Unity project.

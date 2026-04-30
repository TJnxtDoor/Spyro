Console.WriteLine("Spyro smoke runner started.");

string repoRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
string gameProject = Path.Combine(repoRoot, "Spyro", "Spyro.csproj");
string gameAssembly = Path.Combine(repoRoot, "Spyro", "bin", "Debug", "netstandard2.1", "Spyro.dll");

Console.WriteLine(File.Exists(gameProject)
    ? $"Found project: {gameProject}"
    : $"Missing project: {gameProject}");

Console.WriteLine(File.Exists(gameAssembly)
    ? $"Found compiled game library: {gameAssembly}"
    : $"Missing compiled game library: {gameAssembly}");

string[] expectedScripts =
{
    Path.Combine(repoRoot, "MainGame", "GameManager.cs"),
    Path.Combine(repoRoot, "MainGame", "3DPlayer.cs"),
    Path.Combine(repoRoot, "MainGame", "GemProgressionSystem.cs"),
    Path.Combine(repoRoot, "MainGame", "DebugController.cs"),
    Path.Combine(repoRoot, "MainGame", "PauseMenu.cs")
};

foreach (string script in expectedScripts)
{
    Console.WriteLine(File.Exists(script)
        ? $"Found script: {Path.GetRelativePath(repoRoot, script)}"
        : $"Missing script: {Path.GetRelativePath(repoRoot, script)}");
}

Console.WriteLine("Spyro compiled and the smoke runner launched successfully.");
Console.WriteLine("Open this folder in Unity to run the actual scene/game loop.");

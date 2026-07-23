using System.Text.Json;
using System.Xml.Linq;
using NUnit.Framework;

namespace TomeOfTongues.Architecture.Tests;

[TestFixture]
public sealed class ProjectDependencyTests
{
    private static readonly IReadOnlyDictionary<string, string[]> ExpectedProductionDependencies =
        new Dictionary<string, string[]>
        {
            ["TomeOfTongues.Core"] = [],
            ["TomeOfTongues.Application"] = ["TomeOfTongues.Core"],
            ["TomeOfTongues.Content"] = ["TomeOfTongues.Core"],
            ["TomeOfTongues.Content.Tool"] = ["TomeOfTongues.Content"],
            ["TomeOfTongues.Infrastructure"] =
                ["TomeOfTongues.Application", "TomeOfTongues.Content", "TomeOfTongues.Core"]
        };

    private static readonly string[] ExpectedSolutionProjects =
    [
        "TomeOfTongues.Application/TomeOfTongues.Application.csproj",
        "TomeOfTongues.Content.Tool/TomeOfTongues.Content.Tool.csproj",
        "TomeOfTongues.Content/TomeOfTongues.Content.csproj",
        "TomeOfTongues.Core/TomeOfTongues.Core.csproj",
        "TomeOfTongues.Infrastructure/TomeOfTongues.Infrastructure.csproj",
        "tests/TomeOfTongues.Application.Tests/TomeOfTongues.Application.Tests.csproj",
        "tests/TomeOfTongues.Architecture.Tests/TomeOfTongues.Architecture.Tests.csproj",
        "tests/TomeOfTongues.Content.Tests/TomeOfTongues.Content.Tests.csproj",
        "tests/TomeOfTongues.Content.Tool.Tests/TomeOfTongues.Content.Tool.Tests.csproj",
        "tests/TomeOfTongues.Core.Tests/TomeOfTongues.Core.Tests.csproj",
        "tests/TomeOfTongues.Infrastructure.Tests/TomeOfTongues.Infrastructure.Tests.csproj"
    ];

    [Test]
    public void NonMaui_solution_contains_the_complete_generic_project_set()
    {
        var repositoryRoot = FindRepositoryRoot();
        var solutionProjects = XDocument
            .Load(Path.Combine(repositoryRoot, "TomeOfTongues.slnx"))
            .Descendants()
            .Where(element => element.Name.LocalName == "Project")
            .Select(element => element.Attribute("Path")?.Value)
            .Where(path => !string.IsNullOrWhiteSpace(path))
            .Select(path => path!)
            .Select(NormalizeRelativePath)
            .Order()
            .ToArray();

        using var filterDocument = JsonDocument.Parse(
            File.ReadAllText(Path.Combine(repositoryRoot, "TomeOfTongues.NonMaui.slnf")));
        var filterProjects = filterDocument.RootElement
            .GetProperty("solution")
            .GetProperty("projects")
            .EnumerateArray()
            .Select(project => NormalizeRelativePath(project.GetString()!))
            .Order()
            .ToArray();

        Assert.Multiple(() =>
        {
            Assert.That(solutionProjects, Is.EqualTo(ExpectedSolutionProjects.Order()));
            Assert.That(filterProjects, Is.EqualTo(ExpectedSolutionProjects.Order()));
        });
    }

    [Test]
    public void Production_projects_match_the_accepted_dependency_map()
    {
        var repositoryRoot = FindRepositoryRoot();
        var projects = LoadProductionProjects(repositoryRoot);

        var actualProjectNames = projects.Keys.Order().ToArray();
        var expectedProjectNames = ExpectedProductionDependencies.Keys.Order().ToArray();
        Assert.That(actualProjectNames, Is.EqualTo(expectedProjectNames));

        foreach (var (projectName, expectedDependencies) in ExpectedProductionDependencies)
        {
            var actualDependencies = projects[projectName]
                .References
                .Select(Path.GetFileNameWithoutExtension)
                .Order()
                .ToArray();

            Assert.That(
                actualDependencies,
                Is.EqualTo(expectedDependencies.Order()),
                $"{projectName} does not follow the accepted dependency map.");
        }
    }

    [Test]
    public void Generic_projects_do_not_reference_language_specific_code()
    {
        var repositoryRoot = FindRepositoryRoot();
        var forbiddenPrefix = "TomeOfTongues." + "Language.";
        var violations = Directory
            .EnumerateFiles(repositoryRoot, "*", SearchOption.AllDirectories)
            .Where(path => path.EndsWith(".cs", StringComparison.OrdinalIgnoreCase)
                || path.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase))
            .Where(path => !IsGeneratedOrAutomationPath(repositoryRoot, path))
            .Where(path => !IsLanguageProject(repositoryRoot, path))
            .Where(path => File.ReadAllText(path).Contains(forbiddenPrefix, StringComparison.Ordinal))
            .Select(path => Path.GetRelativePath(repositoryRoot, path))
            .Order()
            .ToArray();

        Assert.That(
            violations,
            Is.Empty,
            $"Generic projects must not reference language-specific code:{Environment.NewLine}{string.Join(Environment.NewLine, violations)}");
    }

    private static IReadOnlyDictionary<string, ProjectInfo> LoadProductionProjects(string repositoryRoot) =>
        Directory
            .EnumerateFiles(repositoryRoot, "TomeOfTongues.*.csproj", SearchOption.AllDirectories)
            .Where(path => !IsGeneratedOrAutomationPath(repositoryRoot, path))
            .Where(path => !IsTestProject(repositoryRoot, path))
            .Where(path => !IsLanguageProject(repositoryRoot, path))
            .Select(LoadProject)
            .ToDictionary(project => project.Name, PathComparer);

    private static ProjectInfo LoadProject(string projectPath)
    {
        var fullPath = Path.GetFullPath(projectPath);
        var projectDirectory = Path.GetDirectoryName(fullPath)!;
        var references = XDocument
            .Load(fullPath)
            .Descendants()
            .Where(element => element.Name.LocalName == "ProjectReference")
            .Select(element => element.Attribute("Include")?.Value)
            .Where(include => !string.IsNullOrWhiteSpace(include))
            .Select(include => Path.GetFullPath(Path.Combine(projectDirectory, NormalizePath(include!))))
            .ToArray();

        return new ProjectInfo(Path.GetFileNameWithoutExtension(fullPath), references);
    }

    private static bool IsTestProject(string repositoryRoot, string path) =>
        Path.GetRelativePath(repositoryRoot, path)
            .Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
            .Any(segment => segment.Equals("tests", StringComparison.OrdinalIgnoreCase));

    private static bool IsLanguageProject(string repositoryRoot, string path) =>
        Path.GetRelativePath(repositoryRoot, path)
            .Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
            .Any(segment => segment.StartsWith("TomeOfTongues." + "Language.", StringComparison.OrdinalIgnoreCase));

    private static bool IsGeneratedOrAutomationPath(string repositoryRoot, string path)
    {
        var segments = Path.GetRelativePath(repositoryRoot, path)
            .Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

        return segments.Any(segment =>
            segment.Equals("bin", StringComparison.OrdinalIgnoreCase)
            || segment.Equals("obj", StringComparison.OrdinalIgnoreCase)
            || segment.Equals(".codex-run", StringComparison.OrdinalIgnoreCase)
            || segment.Equals(".git", StringComparison.OrdinalIgnoreCase));
    }

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "TomeOfTongues.slnx")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException(
            "Could not locate the repository root containing TomeOfTongues.slnx.");
    }

    private static string NormalizePath(string path) =>
        path.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);

    private static string NormalizeRelativePath(string path) =>
        path.Replace('\\', '/');

    private static StringComparer PathComparer =>
        OperatingSystem.IsWindows() ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;

    private sealed record ProjectInfo(string Name, IReadOnlyList<string> References);
}

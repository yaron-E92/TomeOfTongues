namespace TomeOfTongues.Content.Schema;

public sealed record TotlangManifest : ITotlangDocument
{
    public required int SchemaVersion { get; init; }
    public required string PackId { get; init; }
    public required string PackageVersion { get; init; }
    public required string MinimumEngineVersion { get; init; }
    public required string LanguageTag { get; init; }
    public required IReadOnlyList<string> LocaleTags { get; init; }
    public required IReadOnlyList<LocalizedText> DisplayNames { get; init; }
    public required IReadOnlyList<RepresentationDefinition> Representations { get; init; }
    public required IReadOnlyList<NormalizationOperation> NormalizationOperations { get; init; }
    public required IReadOnlyList<string> CourseIds { get; init; }
    public required IReadOnlyList<AssetDefinition> Assets { get; init; }
    public required IReadOnlyList<SourceDefinition> Sources { get; init; }
    public required IReadOnlyList<LicenseDefinition> Licenses { get; init; }
}

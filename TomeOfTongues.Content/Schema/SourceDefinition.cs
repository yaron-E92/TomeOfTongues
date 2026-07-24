namespace TomeOfTongues.Content.Schema;

public sealed record SourceDefinition
{
    public required string Id { get; init; }
    public required string Origin { get; init; }
    public required string Author { get; init; }
    public string? Reviewer { get; init; }
    public required string LicenseId { get; init; }
    public required string Attribution { get; init; }
    public required bool RedistributionAllowed { get; init; }
    public required bool ModificationAllowed { get; init; }
    public required DateOnly RecordedOn { get; init; }
}

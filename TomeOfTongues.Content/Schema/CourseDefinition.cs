namespace TomeOfTongues.Content.Schema;

public sealed record CourseDefinition
{
    public required string Id { get; init; }
    public required int Revision { get; init; }
    public required IReadOnlyList<LocalizedText> DisplayNames { get; init; }
    public required string ProficiencyBand { get; init; }
    public required IReadOnlyList<UnitDefinition> Units { get; init; }
}

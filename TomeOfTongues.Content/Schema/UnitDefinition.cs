namespace TomeOfTongues.Content.Schema;

public sealed record UnitDefinition
{
    public required string Id { get; init; }
    public required int Revision { get; init; }
    public required IReadOnlyList<LocalizedText> DisplayNames { get; init; }
    public required IReadOnlyList<string> LessonIds { get; init; }
}

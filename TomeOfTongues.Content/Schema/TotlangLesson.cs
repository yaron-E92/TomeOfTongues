namespace TomeOfTongues.Content.Schema;

public sealed record TotlangLesson : ITotlangDocument
{
    public required int SchemaVersion { get; init; }
    public required string Id { get; init; }
    public required int Revision { get; init; }
    public required string CourseId { get; init; }
    public required string UnitId { get; init; }
    public required IReadOnlyList<LocalizedText> DisplayNames { get; init; }
    public required IReadOnlyList<ObjectiveDefinition> Objectives { get; init; }
    public required IReadOnlyList<ExpressionDefinition> Expressions { get; init; }
    public required IReadOnlyList<StepDefinition> Steps { get; init; }
}

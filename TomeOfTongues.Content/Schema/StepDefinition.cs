namespace TomeOfTongues.Content.Schema;

public sealed record StepDefinition
{
    public required string Id { get; init; }
    public required int Revision { get; init; }
    public required StepKind Kind { get; init; }
    public required StepProgression Progression { get; init; }
    public required IReadOnlyList<string> ExpressionIds { get; init; }
    public ExerciseDefinition? Exercise { get; init; }
    public string? ExternalResourceUri { get; init; }
}

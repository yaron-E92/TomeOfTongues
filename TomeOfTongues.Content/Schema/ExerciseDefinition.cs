namespace TomeOfTongues.Content.Schema;

public sealed record ExerciseDefinition
{
    public required string Id { get; init; }
    public required int Revision { get; init; }
    public required ExerciseType Type { get; init; }
    public required PromptModality PromptModality { get; init; }
    public required ResponseModality ResponseModality { get; init; }
    public required IReadOnlyList<string> TargetIds { get; init; }
    public required IReadOnlyList<AcceptableAnswer> AcceptableAnswers { get; init; }
    public required IReadOnlyList<AssistanceDefinition> Assistance { get; init; }
    public required ScoringDefinition Scoring { get; init; }
    public required IReadOnlyList<EvidenceMapping> Evidence { get; init; }
}

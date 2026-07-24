namespace TomeOfTongues.Content.Schema;

public sealed record ScoringDefinition
{
    public required ScoringMode Mode { get; init; }
    public required double MaximumScore { get; init; }
}

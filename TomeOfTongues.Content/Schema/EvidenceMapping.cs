namespace TomeOfTongues.Content.Schema;

public sealed record EvidenceMapping
{
    public required string ObjectiveId { get; init; }
    public required SkillDimension SkillDimension { get; init; }
    public string? RepresentationId { get; init; }
}

namespace TomeOfTongues.Content.Schema;

public sealed record ObjectiveDefinition
{
    public required string Id { get; init; }
    public required int Revision { get; init; }
    public required SkillDimension SkillDimension { get; init; }
    public string? RepresentationId { get; init; }
}

namespace TomeOfTongues.Content.Schema;

public sealed record AssistanceDefinition
{
    public required string GroupId { get; init; }
    public required AssistanceMode DefaultMode { get; init; }
    public required IReadOnlyList<string> RepresentationIds { get; init; }
}

namespace TomeOfTongues.Content.Schema;

public sealed record TextRepresentation
{
    public required string Id { get; init; }
    public required string RepresentationId { get; init; }
    public required string Value { get; init; }
    public required IReadOnlyList<RangeAnnotation> Annotations { get; init; }
}

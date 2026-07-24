namespace TomeOfTongues.Content.Schema;

public sealed record RangeAnnotation
{
    public required string Id { get; init; }
    public required int Start { get; init; }
    public required int Length { get; init; }
    public required AnnotationKind Kind { get; init; }
    public required string Value { get; init; }
    public string? RepresentationId { get; init; }
}

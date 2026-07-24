namespace TomeOfTongues.Content.Schema;

public sealed record AcceptableAnswer
{
    public required string Value { get; init; }
    public string? RepresentationId { get; init; }
    public required IReadOnlyList<NormalizationOperation> NormalizationOperations { get; init; }
}

namespace TomeOfTongues.Content.Schema;

public sealed record ExpressionDefinition
{
    public required string Id { get; init; }
    public required int Revision { get; init; }
    public required IReadOnlyList<TextRepresentation> Representations { get; init; }
    public required IReadOnlyList<MeaningDefinition> Meanings { get; init; }
    public required IReadOnlyList<AudioReference> Audio { get; init; }
    public required IReadOnlyList<string> SourceIds { get; init; }
}

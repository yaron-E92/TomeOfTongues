namespace TomeOfTongues.Content.Schema;

public sealed record MeaningDefinition
{
    public required MeaningKind Kind { get; init; }
    public required string LanguageTag { get; init; }
    public required string Value { get; init; }
}

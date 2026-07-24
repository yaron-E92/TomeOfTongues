namespace TomeOfTongues.Content.Schema;

public sealed record LocalizedText
{
    public required string LanguageTag { get; init; }
    public required string Value { get; init; }
}

namespace TomeOfTongues.Content.Schema;

public sealed record RepresentationDefinition
{
    public required string Id { get; init; }
    public required string LanguageTag { get; init; }
    public required string ScriptTag { get; init; }
    public required TextDirection Direction { get; init; }
    public string? TransliterationScheme { get; init; }
    public string? AssistanceGroupId { get; init; }
}

namespace TomeOfTongues.Content.Schema;

public sealed record AssetDefinition
{
    public required string Id { get; init; }
    public required string Path { get; init; }
    public required string MediaType { get; init; }
    public required string Sha256 { get; init; }
    public string? SourceId { get; init; }
}

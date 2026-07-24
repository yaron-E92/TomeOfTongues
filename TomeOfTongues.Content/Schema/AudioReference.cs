namespace TomeOfTongues.Content.Schema;

public sealed record AudioReference
{
    public required string AssetId { get; init; }
    public string? TranscriptRepresentationId { get; init; }
}

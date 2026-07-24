namespace TomeOfTongues.Content.Schema;

public sealed record LicenseDefinition
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public string? Uri { get; init; }
}

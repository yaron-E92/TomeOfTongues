namespace TomeOfTongues.Content.Schema;

public sealed record TotlangCourseCatalog : ITotlangDocument
{
    public required int SchemaVersion { get; init; }
    public required IReadOnlyList<CourseDefinition> Courses { get; init; }
}

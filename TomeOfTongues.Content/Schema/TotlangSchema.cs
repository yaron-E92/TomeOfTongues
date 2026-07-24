using System.Text.Json;
using System.Text.Json.Serialization;

namespace TomeOfTongues.Content.Schema;

public interface ITotlangDocument
{
    int SchemaVersion { get; }
}

public static class TotlangSchema
{
    public const int CurrentVersion = 1;

    private static readonly JsonSerializerOptions SerializerOptions = CreateSerializerOptions();

    public static string Serialize<TDocument>(TDocument document)
        where TDocument : class, ITotlangDocument
    {
        ArgumentNullException.ThrowIfNull(document);
        ValidateIntrinsicContract(document);
        return JsonSerializer.Serialize(document, SerializerOptions);
    }

    public static TDocument Deserialize<TDocument>(string json)
        where TDocument : class, ITotlangDocument
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(json);
        EnsureSupportedSchemaVersion(json);

        try
        {
            var document = JsonSerializer.Deserialize<TDocument>(json, SerializerOptions)
                ?? throw new TotlangSchemaException("The document must contain a JSON object.");
            ValidateIntrinsicContract(document);
            return document;
        }
        catch (JsonException exception)
        {
            throw new TotlangSchemaException("The document does not match the .totlang schema.", exception);
        }
    }

    private static JsonSerializerOptions CreateSerializerOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
        };
        options.Converters.Add(new JsonStringEnumConverter(
            JsonNamingPolicy.CamelCase,
            allowIntegerValues: false));
        return options;
    }

    private static void EnsureSupportedSchemaVersion(string json)
    {
        try
        {
            using var document = JsonDocument.Parse(json);
            if (document.RootElement.ValueKind is not JsonValueKind.Object
                || !document.RootElement.TryGetProperty("schemaVersion", out var versionElement)
                || versionElement.ValueKind is not JsonValueKind.Number
                || !versionElement.TryGetInt32(out var version))
            {
                throw new TotlangSchemaException(
                    "The document must declare an integer schemaVersion.");
            }

            if (version != CurrentVersion)
            {
                throw new TotlangSchemaException(
                    $"Unsupported .totlang schema version {version}. Expected {CurrentVersion}.");
            }
        }
        catch (JsonException exception)
        {
            throw new TotlangSchemaException("The document is not valid JSON.", exception);
        }
    }

    private static void ValidateIntrinsicContract(ITotlangDocument document)
    {
        if (document.SchemaVersion != CurrentVersion)
        {
            throw new TotlangSchemaException(
                $"Unsupported .totlang schema version {document.SchemaVersion}. Expected {CurrentVersion}.");
        }

        if (document is not TotlangLesson lesson)
        {
            return;
        }

        foreach (var expression in lesson.Expressions)
        {
            foreach (var representation in expression.Representations)
            {
                foreach (var annotation in representation.Annotations)
                {
                    if (annotation.Start < 0
                        || annotation.Length <= 0
                        || annotation.Start > representation.Value.Length - annotation.Length)
                    {
                        throw new TotlangSchemaException(
                            $"Annotation '{annotation.Id}' is outside representation '{representation.Id}'.");
                    }
                }
            }
        }

        foreach (var step in lesson.Steps)
        {
            if (step.Kind is StepKind.OptionalSpeakingOpportunity
                && step.Progression is not StepProgression.DeferredAllowed)
            {
                throw new TotlangSchemaException(
                    $"Speaking step '{step.Id}' must allow deferral.");
            }

            if (step.Exercise?.ResponseModality is ResponseModality.Spoken
                && step.Progression is not StepProgression.DeferredAllowed)
            {
                throw new TotlangSchemaException(
                    $"Spoken exercise '{step.Exercise.Id}' must allow deferral.");
            }
        }
    }
}

public sealed class TotlangSchemaException : Exception
{
    public TotlangSchemaException(string message)
        : base(message)
    {
    }

    public TotlangSchemaException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

public sealed record TotlangManifest : ITotlangDocument
{
    public required int SchemaVersion { get; init; }
    public required string PackId { get; init; }
    public required string PackageVersion { get; init; }
    public required string MinimumEngineVersion { get; init; }
    public required string LanguageTag { get; init; }
    public required IReadOnlyList<string> LocaleTags { get; init; }
    public required IReadOnlyList<LocalizedText> DisplayNames { get; init; }
    public required IReadOnlyList<RepresentationDefinition> Representations { get; init; }
    public required IReadOnlyList<NormalizationOperation> NormalizationOperations { get; init; }
    public required IReadOnlyList<string> CourseIds { get; init; }
    public required IReadOnlyList<AssetDefinition> Assets { get; init; }
    public required IReadOnlyList<SourceDefinition> Sources { get; init; }
    public required IReadOnlyList<LicenseDefinition> Licenses { get; init; }
}

public sealed record TotlangCourseCatalog : ITotlangDocument
{
    public required int SchemaVersion { get; init; }
    public required IReadOnlyList<CourseDefinition> Courses { get; init; }
}

public sealed record TotlangLesson : ITotlangDocument
{
    public required int SchemaVersion { get; init; }
    public required string Id { get; init; }
    public required int Revision { get; init; }
    public required string CourseId { get; init; }
    public required string UnitId { get; init; }
    public required IReadOnlyList<LocalizedText> DisplayNames { get; init; }
    public required IReadOnlyList<ObjectiveDefinition> Objectives { get; init; }
    public required IReadOnlyList<ExpressionDefinition> Expressions { get; init; }
    public required IReadOnlyList<StepDefinition> Steps { get; init; }
}

public sealed record LocalizedText
{
    public required string LanguageTag { get; init; }
    public required string Value { get; init; }
}

public sealed record RepresentationDefinition
{
    public required string Id { get; init; }
    public required string LanguageTag { get; init; }
    public required string ScriptTag { get; init; }
    public required TextDirection Direction { get; init; }
    public string? TransliterationScheme { get; init; }
    public string? AssistanceGroupId { get; init; }
}

public sealed record AssetDefinition
{
    public required string Id { get; init; }
    public required string Path { get; init; }
    public required string MediaType { get; init; }
    public required string Sha256 { get; init; }
    public string? SourceId { get; init; }
}

public sealed record SourceDefinition
{
    public required string Id { get; init; }
    public required string Origin { get; init; }
    public required string Author { get; init; }
    public string? Reviewer { get; init; }
    public required string LicenseId { get; init; }
    public required string Attribution { get; init; }
    public required bool RedistributionAllowed { get; init; }
    public required bool ModificationAllowed { get; init; }
    public required DateOnly RecordedOn { get; init; }
}

public sealed record LicenseDefinition
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public string? Uri { get; init; }
}

public sealed record CourseDefinition
{
    public required string Id { get; init; }
    public required int Revision { get; init; }
    public required IReadOnlyList<LocalizedText> DisplayNames { get; init; }
    public required string ProficiencyBand { get; init; }
    public required IReadOnlyList<UnitDefinition> Units { get; init; }
}

public sealed record UnitDefinition
{
    public required string Id { get; init; }
    public required int Revision { get; init; }
    public required IReadOnlyList<LocalizedText> DisplayNames { get; init; }
    public required IReadOnlyList<string> LessonIds { get; init; }
}

public sealed record ObjectiveDefinition
{
    public required string Id { get; init; }
    public required int Revision { get; init; }
    public required SkillDimension SkillDimension { get; init; }
    public string? RepresentationId { get; init; }
}

public sealed record ExpressionDefinition
{
    public required string Id { get; init; }
    public required int Revision { get; init; }
    public required IReadOnlyList<TextRepresentation> Representations { get; init; }
    public required IReadOnlyList<MeaningDefinition> Meanings { get; init; }
    public required IReadOnlyList<AudioReference> Audio { get; init; }
    public required IReadOnlyList<string> SourceIds { get; init; }
}

public sealed record TextRepresentation
{
    public required string Id { get; init; }
    public required string RepresentationId { get; init; }
    public required string Value { get; init; }
    public required IReadOnlyList<RangeAnnotation> Annotations { get; init; }
}

public sealed record RangeAnnotation
{
    public required string Id { get; init; }
    public required int Start { get; init; }
    public required int Length { get; init; }
    public required AnnotationKind Kind { get; init; }
    public required string Value { get; init; }
    public string? RepresentationId { get; init; }
}

public sealed record MeaningDefinition
{
    public required MeaningKind Kind { get; init; }
    public required string LanguageTag { get; init; }
    public required string Value { get; init; }
}

public sealed record AudioReference
{
    public required string AssetId { get; init; }
    public string? TranscriptRepresentationId { get; init; }
}

public sealed record StepDefinition
{
    public required string Id { get; init; }
    public required int Revision { get; init; }
    public required StepKind Kind { get; init; }
    public required StepProgression Progression { get; init; }
    public required IReadOnlyList<string> ExpressionIds { get; init; }
    public ExerciseDefinition? Exercise { get; init; }
    public string? ExternalResourceUri { get; init; }
}

public sealed record ExerciseDefinition
{
    public required string Id { get; init; }
    public required int Revision { get; init; }
    public required ExerciseType Type { get; init; }
    public required PromptModality PromptModality { get; init; }
    public required ResponseModality ResponseModality { get; init; }
    public required IReadOnlyList<string> TargetIds { get; init; }
    public required IReadOnlyList<AcceptableAnswer> AcceptableAnswers { get; init; }
    public required IReadOnlyList<AssistanceDefinition> Assistance { get; init; }
    public required IReadOnlyList<EvidenceMapping> Evidence { get; init; }
}

public sealed record AcceptableAnswer
{
    public required string Value { get; init; }
    public string? RepresentationId { get; init; }
    public required IReadOnlyList<NormalizationOperation> NormalizationOperations { get; init; }
}

public sealed record AssistanceDefinition
{
    public required string GroupId { get; init; }
    public required AssistanceMode DefaultMode { get; init; }
    public required IReadOnlyList<string> RepresentationIds { get; init; }
}

public sealed record EvidenceMapping
{
    public required string ObjectiveId { get; init; }
    public required SkillDimension SkillDimension { get; init; }
    public string? RepresentationId { get; init; }
}

public enum TextDirection
{
    LeftToRight,
    RightToLeft
}

public enum NormalizationOperation
{
    UnicodeNfc,
    UnicodeNfkc,
    Trim,
    CollapseWhitespace,
    CaseFold,
    IgnorePunctuation
}

public enum AnnotationKind
{
    Reading,
    Pronunciation,
    Transliteration,
    Vowel,
    Usage
}

public enum MeaningKind
{
    Natural,
    Literal
}

public enum StepKind
{
    Explanation,
    ContextualExposure,
    Exercise,
    Checkpoint,
    ExternalResource,
    OptionalSpeakingOpportunity
}

public enum StepProgression
{
    Required,
    Optional,
    DeferredAllowed
}

public enum ExerciseType
{
    Selection,
    Matching,
    Ordering,
    TextEntry,
    SelfAssessment
}

public enum PromptModality
{
    Text,
    Audio,
    Image
}

public enum ResponseModality
{
    Selection,
    Typed,
    Spoken,
    SelfReported
}

public enum AssistanceMode
{
    AlwaysShow,
    Adaptive,
    TapToReveal,
    NeverShow
}

public enum SkillDimension
{
    Recognition,
    Recall,
    ListeningComprehension,
    ReadingRecognition,
    SilentProduction,
    SpokenProduction
}

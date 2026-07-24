namespace TomeOfTongues.Content.Schema;

public enum NormalizationOperation
{
    UnicodeNfc,
    UnicodeNfkc,
    Trim,
    CollapseWhitespace,
    CaseFold,
    IgnorePunctuation
}

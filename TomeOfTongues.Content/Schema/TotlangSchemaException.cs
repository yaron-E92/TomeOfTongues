namespace TomeOfTongues.Content.Schema;

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

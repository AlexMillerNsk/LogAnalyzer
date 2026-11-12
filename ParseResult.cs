namespace LogAnalyzer;

public abstract record ParseResult
{
    private ParseResult() { }

    public sealed record Parsed(LogEntry Entry) : ParseResult;
    public sealed record Skipped(string Reason) : ParseResult;
    public sealed record Malformed(string RawLine, string Reason) : ParseResult;
}
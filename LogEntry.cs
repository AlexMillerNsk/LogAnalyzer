namespace LogAnalyzer;

public record LogEntry(DateTime Timestamp, Severity Level, string Service, string Message, string? RawException = null)
{ }
namespace LogAnalyzer;

public static class LogEntryExtensions
{
    public static IEnumerable<LogEntry> OnlyAtLeast(this IEnumerable<LogEntry> entries, Severity minimum) =>
        entries.Where(e => e.Level.CompareTo(minimum) >= 0);

    public static IEnumerable<LogEntry> InService(this IEnumerable<LogEntry> entries, string service) =>
        entries.Where(e => e.Service == service);

    public static IEnumerable<LogEntry> InTimeRange(this IEnumerable<LogEntry> entries, DateTime from, DateTime to) =>
        entries.Where(e => e.Timestamp >= from && e.Timestamp < to);
}
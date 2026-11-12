namespace LogAnalyzer;

public readonly struct Severity : IComparable<Severity>, IEquatable<Severity>
{
    public static readonly Severity Info = new(0, "INFO");
    public static readonly Severity Warning = new(1, "WARN");
    public static readonly Severity Error = new(2, "ERROR");
    public static readonly Severity Critical = new(3, "CRITICAL");

    public int Rank { get; }
    public string Name { get; }

    private Severity(int rank, string name)
    {
        Rank = rank;
        Name = name;
    }

    public static Severity Parse(string raw) => raw switch
    {
        "INFO" => Info,
        "WARN" => Warning,
        "ERROR" => Error,
        "CRITICAL" => Critical,
        _ => throw new FormatException($"Неизвестный уровень лога: {raw}"),
    };

    public int CompareTo(Severity other) => Rank.CompareTo(other.Rank);
    public bool Equals(Severity other) => Rank == other.Rank;
    public override bool Equals(object? obj) => obj is Severity s && Equals(s);
    public override int GetHashCode() => Rank;
    public override string ToString() => Name;

    public static bool operator <(Severity a, Severity b) => a.Rank < b.Rank;
    public static bool operator >(Severity a, Severity b) => a.Rank > b.Rank;
    public static bool operator ==(Severity a, Severity b) => a.Rank == b.Rank;
    public static bool operator !=(Severity a, Severity b) => a.Rank != b.Rank;
    
    public static LogEntry Redact(LogEntry entry) =>
        entry.Service == "PaymentService" && entry.Level == Critical
            ? entry with { Message = "[REDACTED]" }
            : entry;
}
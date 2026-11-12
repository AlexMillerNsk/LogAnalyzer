using LogAnalyzer;

foreach (var line in File.ReadLines("sample.log"))
{
    var outcome = LogParser.TryParseLine(line);

    var report = outcome switch
    {
        ParseResult.Parsed p => $"OK: {p.Entry.Service} — {p.Entry.Message}",
        ParseResult.Skipped s => $"Пропущено: {s.Reason}",
        ParseResult.Malformed m => $"Повреждена ({m.Reason}): {m.RawLine}",
        _ => throw new InvalidOperationException("Неизвестный вариант ParseResult"),
    };

    Console.WriteLine(report);
}
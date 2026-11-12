namespace LogAnalyzer;

public static class LogParser
{
        public static ParseResult TryParseLine(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            return new ParseResult.Skipped("Пустая строка");

        if (line.StartsWith('#'))
            return new ParseResult.Skipped("Комментарий");

        var parts = line.Split(' ', 5);
        if (parts.Length < 5)
            return new ParseResult.Malformed(line, "Недостаточно полей");

        if (!DateTime.TryParse($"{parts[0]} {parts[1]}", out var timestamp))
            return new ParseResult.Malformed(line, "Не распознана дата/время");

        Severity level;
        try
        {
            level = Severity.Parse(parts[2]);
        }
        catch (FormatException)
        {
            return new ParseResult.Malformed(line, $"Неизвестный уровень: {parts[2]}");
        }

        var serviceToken = parts[3];
        if (serviceToken.Length < 2 || serviceToken[0] != '[' || serviceToken[^1] != ']')
            return new ParseResult.Malformed(line, "Сервис не в квадратных скобках");
        

        return new ParseResult.Parsed(new LogEntry(timestamp, level, serviceToken[1..^1], parts[4]));
    }
}
using System.Text.RegularExpressions;
using SparkLogViewer.Core.Enums;
using SparkLogViewer.Core.Models;
using SparkLogViewer.Core.Services.Interfaces;

namespace SparkLogViewer.Core.Parsers;

public partial class MinecraftLogParser : ILogParser
{
    private static readonly Regex LogRegex = LogRegexFunc();

    /// <inheritdoc />
    public bool TryParse(string line, ulong lineNumber, out LogEntry entry)
    {
        var match = LogRegex.Match(line);
        if (match.Success)
        {
            entry = new LogEntry
            {
                LineNumber = lineNumber,
                OriginalContent = line,
                Timestamp = DateTime.Parse(match.Groups[1].Value),
                ThreadName = match.Groups[2].Value,
                Level = ParserLogLevel(match.Groups[3].Value),
                Message = match.Groups[4].Value.Trim()
            };
            return true;
        }

        entry = new LogEntry
        {
            LineNumber = lineNumber,
            OriginalContent = line,
            Level = LogLevel.Unknown,
            Message = line
        };

        return true;
    }

    private static LogLevel ParserLogLevel(string level)
    {
        return level.ToUpperInvariant() switch
        {
            "INFO" => LogLevel.Info,
            "WARN" => LogLevel.Warning,
            "ERROR" => LogLevel.Error,
            "FATAL" => LogLevel.Fatal,
            "DEBUG" => LogLevel.Debug,
            _ => LogLevel.Unknown
        };
    }

    [GeneratedRegex(@"^\[(\d{2}:\d{2}:\d{2})\] \[([^/]+)\/(\w+)\]: (.*)$", RegexOptions.Compiled)]
    private static partial Regex LogRegexFunc();
}

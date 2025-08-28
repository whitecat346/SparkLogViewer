using SparkLogViewer.Core.Enums;

namespace SparkLogViewer.Core.Models;

public class LogEntry
{
    public ulong LineNumber { get; set; }
    public string? OriginalContent { get; set; } = string.Empty;
    public string ThreadName { get; set; } = string.Empty;
    public LogLevel Level { get; set; }
    public DateTime Timestamp { get; set; }
    public string? Message { get; set; } = string.Empty;
}

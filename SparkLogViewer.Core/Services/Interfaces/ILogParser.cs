using SparkLogViewer.Core.Models;

namespace SparkLogViewer.Core.Services.Interfaces;

public interface ILogParser
{
    bool TryParse(string line, ulong lineNumber, out LogEntry entry);
}

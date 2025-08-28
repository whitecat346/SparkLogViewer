using System.Runtime.CompilerServices;
using SparkLogViewer.Core.Services.Interfaces;

namespace SparkLogViewer.Infrastructure.Services.Implementations;

public class LogFileReaderServices : ILogReaderService
{
    /// <inheritdoc />
    public async IAsyncEnumerable<string> ReadLinesAsync(string filePath,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var reader = new StreamReader(filePath);
        while (await reader.ReadLineAsync(cancellationToken) is { } line)
        {
            yield return line;
        }
    }
}
namespace SparkLogViewer.Core.Services.Interfaces;

public interface ILogReaderService
{
    IAsyncEnumerable<string> ReadLinesAsync(string filePath,
        CancellationToken cancellationToken = default);
}

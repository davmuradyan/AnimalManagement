using Microsoft.Extensions.Logging;

namespace AnimalManagement.Shared.Logging;

public class FileLoggerProvider(
    string filePath,
    LogLevel minimumLevel = LogLevel.Trace)
    : ILoggerProvider
{
    private readonly FileLogWriter _writer = new(filePath);

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(
            categoryName,
            _writer,
            minimumLevel);
    }

    public void Dispose()
    {
        _writer.Dispose();
    }
}
using Microsoft.Extensions.Logging;

namespace AnimalManagement.Shared.Logging;

public class FileLogger(string category, FileLogWriter writer, LogLevel minimumLevel) : ILogger
{
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= minimumLevel && logLevel != LogLevel.None;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        var message = formatter(state, exception);

        var logEntry =
            $"[{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss.fff zzz}] " +
            $"[{logLevel}] [{category}] {message}";

        if (exception is not null)
            logEntry += Environment.NewLine + exception;

        writer.Write(logEntry);
    }
}
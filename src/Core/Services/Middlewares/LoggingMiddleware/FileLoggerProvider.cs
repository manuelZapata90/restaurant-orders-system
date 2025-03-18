using System;

namespace Services.Middlewares.LoggingMiddleware;

using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;

public class FileLoggerProvider : ILoggerProvider
{
    private readonly string _filePath;
    private readonly LogLevel _minLevel;

    public FileLoggerProvider(string path, LogLevel minLevel)
    {
        _filePath = path.Replace("{Date}", DateTime.Now.ToString("yyyyMMdd"));
        _minLevel = minLevel;
        EnsureDirectoryExists();
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(_filePath, _minLevel);
    }

    public void Dispose() { }

    private void EnsureDirectoryExists()
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
}

public class FileLogger : ILogger
{
    private readonly string _filePath;
    private readonly LogLevel _minLevel;
    private static readonly object _lock = new object();

    public FileLogger(string path, LogLevel minLevel)
    {
        _filePath = path;
        _minLevel = minLevel;
    }

    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => logLevel >= _minLevel;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception exception,
        Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        var message = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {formatter(state, exception)}{Environment.NewLine}";

        lock (_lock)
        {
            File.AppendAllText(_filePath, message, Encoding.UTF8);
        }
    }
}
using Microsoft.Extensions.Logging;

namespace Services.Middlewares.LoggingMiddleware;

public class LoggingSettings
{
    public LogLevel DefaultLogLevel { get; set; } = LogLevel.Information;
    public bool EnableRequestLogging { get; set; } = true;
    public bool EnableErrorLogging { get; set; } = true;
    public List<string> LogProperties { get; set; } = new();
    public Dictionary<string, LogLevel> ProjectLogLevels { get; set; } = new();
    public FileLoggerSettings File {get;set;}

}
public class FileLoggerSettings{
    public string Path {get;set;}
    public string MinimumLevel {get;set;}
}

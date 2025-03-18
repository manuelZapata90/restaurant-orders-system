using System;

namespace Services.Middlewares.LoggingMiddleware;
public class LogContext
{
    public string Project { get; set; }
    public string Module { get; set; }
    public string User { get; set; }
    public Dictionary<string, object> CustomProperties { get; } = new();
}
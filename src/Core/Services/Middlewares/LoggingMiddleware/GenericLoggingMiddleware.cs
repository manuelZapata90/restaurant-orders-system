using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Services.Middlewares.LoggingMiddleware;

public class GenericLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GenericLoggingMiddleware> _logger;
    private readonly LoggingSettings _settings;
    private readonly Func<HttpContext, LogContext> _contextProvider;

    public GenericLoggingMiddleware(
        RequestDelegate next,
        ILogger<GenericLoggingMiddleware> logger,
        IConfiguration configuration,
        Func<HttpContext, LogContext> contextProvider = null)
    {
        _next = next;
        _logger = logger;
        _settings = configuration.GetSection("Logging").Get<LoggingSettings>();
        _contextProvider = contextProvider ?? DefaultContextProvider;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var logContext = _contextProvider(context);
        var logLevel = GetLogLevel(logContext.Project);
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(context);
            stopwatch.Stop();

            if (ShouldLog(logLevel, LogLevel.Information) && _settings.EnableRequestLogging)
            {
                LogRequest(logContext, stopwatch.ElapsedMilliseconds);
            }
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            
            if (ShouldLog(logLevel, LogLevel.Error) && _settings.EnableErrorLogging)
            {
                LogError(logContext, ex, stopwatch.ElapsedMilliseconds);
            }
            throw;
        }
    }

    private void LogRequest(LogContext context, long durationMs)
    {
        var logData = BuildLogData(context, durationMs);
        _logger.Log(LogLevel.Information, "Request: {@LogData}", logData);
    }

    private void LogError(LogContext context, Exception ex, long durationMs)
    {
        var logData = BuildLogData(context, durationMs);
        _logger.Log(LogLevel.Error, ex, "Error: {@LogData}", logData);
    }

    private Dictionary<string, object> BuildLogData(LogContext context, long durationMs)
    {
        var logData = new Dictionary<string, object>
        {
            ["DurationMs"] = durationMs
        };

        foreach (var prop in _settings.LogProperties)
        {
            if (prop == "Project") logData[prop] = context.Project;
            if (prop == "Module") logData[prop] = context.Module;
            if (prop == "User") logData[prop] = context.User;
        }

        foreach (var customProp in context.CustomProperties)
        {
            logData[customProp.Key] = customProp.Value;
        }

        return logData;
    }

    private LogLevel GetLogLevel(string project)
    {
        return _settings.ProjectLogLevels.TryGetValue(project, out var level) 
            ? level 
            : _settings.DefaultLogLevel;
    }

    private bool ShouldLog(LogLevel configuredLevel, LogLevel desiredLevel)
    {
        return configuredLevel <= desiredLevel;
    }

    private static LogContext DefaultContextProvider(HttpContext context)
    {
        return new LogContext
        {
            Project = "Api",
            Module = context.Request.Path,
            User = context.User.Identity?.Name
        };
    }
}
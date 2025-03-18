using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Services.Middlewares.LoggingMiddleware;

public static class LoggingExtensions
{
    public static IServiceCollection AddCrossPlatformLogging(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<LoggingSettings> configureSettings = null)
    {
        var settings = configuration.GetSection("Logging").Get<LoggingSettings>();
        configureSettings?.Invoke(settings);
        services.AddLogging(loggingBuilder => 
        {
            loggingBuilder.AddProvider(new FileLoggerProvider(
                "Logs/Api-{Date}.txt",
                Enum.Parse<LogLevel>("Information"))
            );
        });
        services.AddSingleton(settings);
        return services;
    }

    public static IApplicationBuilder UseCrossPlatformLogging(
        this IApplicationBuilder app,
        Func<HttpContext, LogContext> contextProvider = null)
    {
        return app.UseMiddleware<GenericLoggingMiddleware>(contextProvider);
    }
}
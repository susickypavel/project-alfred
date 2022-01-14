using Discord;
using Microsoft.Extensions.Logging;

namespace project_alfred.services;

public class CustomLogger
{
    private readonly ILogger<CustomLogger> _logger;

    public CustomLogger(ILogger<CustomLogger> logger)
    {
        _logger = logger;
    }

    public Task Log(LogMessage msg)
    {
        _logger.Log(ConvertMessageSeverity(msg.Severity), $"{msg.Message}");
        
        return Task.CompletedTask;
    }

    private LogLevel ConvertMessageSeverity(LogSeverity severity)
    {
        switch (severity)
        {
            case LogSeverity.Info:
                return LogLevel.Information;
            case LogSeverity.Warning:
                return LogLevel.Warning;
            case LogSeverity.Error:
                return LogLevel.Error;
            case LogSeverity.Critical:
                return LogLevel.Critical;
            case LogSeverity.Debug:
                return LogLevel.Debug;
            case LogSeverity.Verbose:
                return LogLevel.Trace;
            default:
                _logger.LogCritical($"Unknown discord log severity '{severity}'. Using 'Debug' level.");
                return LogLevel.Debug;
        }
    }
}
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace project_alfred.Services;

public class DiscordClientService : IHostedService
{
    private readonly ILogger<DiscordClientService> _logger;
    private readonly DiscordSocketClient _client;

    public DiscordClientService(ILogger<DiscordClientService> logger, IConfiguration configuration)
    {

        _logger = logger;
        _client = new DiscordSocketClient();
        _client.Log += Log;
        
        _client.LoginAsync(TokenType.Bot, configuration["Bot:TOKEN"]);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _client.StartAsync();
        _logger.LogInformation("Successfully started");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _client.LogoutAsync();
        _logger.LogInformation("Successfully stopped");
    }
    
    private Task Log(LogMessage arg)
    {
        _logger.Log(ConvertLogSeverityToLevel(arg.Severity), "{Arg.Message}", arg.Message);
        return Task.CompletedTask;
    }

    private LogLevel ConvertLogSeverityToLevel(LogSeverity severity)
    {
        switch (severity)
        {
            case LogSeverity.Critical:
                return LogLevel.Critical;
            case LogSeverity.Error:
                return LogLevel.Error;
            case LogSeverity.Warning:
                return LogLevel.Warning;
            case LogSeverity.Info:
                return LogLevel.Information;
            case LogSeverity.Verbose:
                return LogLevel.Trace;
            case LogSeverity.Debug:
                return LogLevel.Debug;
            default:
                _logger.LogWarning("Unknown severity '{Severity}'", severity);
                return LogLevel.None;
        }
    }
}
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace project_alfred.services;

public class DiscordClient : IHostedService
{
    private readonly ILogger<DiscordClient> _logger;
    private readonly IConfiguration _config;

    private readonly DiscordSocketClient _client = new(new DiscordSocketConfig()
    {
        // BUG: Currently, program stops sending reconnection request to Discord API, but the program doesn't exit.
        ConnectionTimeout = 20_000
    });

    public DiscordClient(ILogger<DiscordClient> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _client.LoginAsync(TokenType.Bot, _config.GetValue<string>("DISCORD_BOT_TOKEN"));
        _client.StartAsync();

        _client.Log += Log; 

        _logger.LogInformation("Connection successful.");
        return Task.CompletedTask;
    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _client.StopAsync();
        
        _logger.LogInformation("Ending Discord Service now.");
        return Task.CompletedTask;
    }
    
    /**
     * NOTE: This was supposed to be extracted to a ICustomLogger, but the level of complexity it brings is overwhelming.
     *
     * However, it should probably be refactored later (if needed)
     */
    private Task Log(LogMessage arg)
    {
        _logger.Log(ConvertLogSeverityToLevel(arg.Severity), $"[{arg.Source}]: {arg.Message}");

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
                _logger.LogWarning($"Unknown severity '{severity}'.");
                return LogLevel.None;
        }
    }
}
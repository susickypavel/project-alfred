using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace project_alfred.services;

public class DiscordClient : IHostedService
{
    private readonly ILogger<DiscordClient> _logger;
    private readonly CustomLogger _customLogger;
    private readonly IConfiguration _config;

    private readonly DiscordSocketClient _client = new(new DiscordSocketConfig()
    {
        // BUG: Currently, program stops sending reconnection request to Discord API, but the program doesn't exit.
        ConnectionTimeout = 20_000
    });

    public DiscordClient(ILogger<DiscordClient> logger, IConfiguration config, CustomLogger customLogger)
    {
        _logger = logger;
        _customLogger = customLogger;
        _config = config;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _client.LoginAsync(TokenType.Bot, _config.GetValue<string>("DISCORD_BOT_TOKEN"));
        _client.StartAsync();

        _client.Log += _customLogger.Log; 

        _logger.LogInformation("Connection successful.");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _client.StopAsync();
        
        _logger.LogInformation("Ending Discord Service now.");
        return Task.CompletedTask;
    }
}
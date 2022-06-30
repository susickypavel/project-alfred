﻿using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace project_alfred.Services;

public class DiscordClientService : IHostedService
{
    private readonly ILogger<DiscordClientService> _logger;
    private readonly DiscordSocketClient _client;
    private readonly CommandService _command;
    private readonly IServiceProvider _services;

    private readonly string _token;

    public DiscordClientService(ILogger<DiscordClientService> logger, IConfiguration configuration, CommandService command, IServiceProvider services)
    {
        _token = configuration["DISCORD_BOT_TOKEN"];
        _logger = logger;
        _command = command;
        _services = services;
        _client = new DiscordSocketClient();
        _client.Log += Log;
        _client.MessageReceived += HandleCommand;
    }

    private async Task HandleCommand(SocketMessage msg)
    {
        var message = msg as SocketUserMessage;
        if (message == null) return;

        var argPos = 0;
        
        if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)) || message.Author.IsBot)
            return;

        var context = new SocketCommandContext(_client, message);

        await _command.ExecuteAsync(context, argPos, _services);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _client.LoginAsync(TokenType.Bot, _token);
        await _client.StartAsync();
        await _command.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
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
                _logger.LogWarning("Unknown severity '{Severity}'", Enum.GetName(severity));
                return LogLevel.None;
        }
    }
}
using Discord;
using Discord.WebSocket;

namespace project_alfred;

public class Program
{
    private DiscordSocketClient? _client;
    
    public static Task Main(string[] args)
    {
        return new Program().MainAsync();
    }

    private async Task MainAsync()
    {
        _client = new DiscordSocketClient();
        _client.Log += Log;
        
        await _client.LoginAsync(TokenType.Bot, "TODO");
        await _client.StartAsync();
        
        await Task.Delay(-1);
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}
using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace project_alfred.Services
{
    public class LogService
    {
        public LogService(DiscordSocketClient client, CommandService command)
        {
            client.Log += Log;
            command.Log += Log;
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine($@"[{msg.Severity}] - {msg.Message}");
            return Task.CompletedTask;
        }
    }
}
using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace project_alfred.Services
{
    public class LogService
    {
        public LogService(DiscordSocketClient client)
        {
            client.Log += Log;
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
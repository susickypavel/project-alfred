using System;
using System.Threading.Tasks;

using Discord;

namespace project_alfred.Services
{
    public class LoggerService
    {
        public Task Log(LogMessage msg)
        {
            Console.WriteLine($"[{msg.Severity}] {msg.Message}");

            if (msg.Exception != null)
            {
                Console.WriteLine(msg.Exception);
            }
            
            return Task.CompletedTask;
        }
    }
}
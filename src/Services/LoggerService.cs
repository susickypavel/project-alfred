using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Discord;

namespace project_alfred.Services
{
    public class LoggerService
    {
        private readonly Dictionary<LogSeverity, ConsoleColor> _colors = new Dictionary<LogSeverity, ConsoleColor>()
        {
            {LogSeverity.Critical, ConsoleColor.DarkRed},
            {LogSeverity.Error, ConsoleColor.Red},
            {LogSeverity.Warning, ConsoleColor.Yellow},
            {LogSeverity.Info, ConsoleColor.Blue},
            {LogSeverity.Verbose, ConsoleColor.Green},
            {LogSeverity.Debug, ConsoleColor.Cyan},
        };
        
        public Task Log(LogMessage msg)
        {
            Console.Write("[");
            Console.ForegroundColor = _colors[msg.Severity];
            Console.Write($"{msg.Severity}");
            Console.ResetColor();
            Console.WriteLine($"] {msg.Message}");
            
            if (msg.Exception != null)
            {
                Console.WriteLine(msg.Exception);
            }
            
            return Task.CompletedTask;
        }
    }
}
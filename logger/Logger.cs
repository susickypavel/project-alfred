using Discord;

namespace project_alfred.logger;

public class Logger
{
    public Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}
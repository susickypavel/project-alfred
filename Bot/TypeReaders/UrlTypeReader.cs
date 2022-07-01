using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace project_alfred.TypeReaders;

public class Url
{
    public Url(string value)
    {
        Value = value;
    }

    public string Value { get; }
}

public class UrlTypeReader : TypeReader
{
    public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILogger<UrlTypeReader>>();
        
        if (input.StartsWith("https://"))
        {
            return Task.FromResult(TypeReaderResult.FromSuccess(new Url(input)));
        }
        
        logger.LogInformation("Invalid argument '{Input}' for type Url", input);
        return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed, "Input could not be parsed as a URL."));
    }
}
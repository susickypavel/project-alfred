using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord.Commands;

namespace project_alfred.TypeReaders
{
    public readonly struct Url
    {
        public string Value { get; }

        public Url(string value)
        {
            Value = value;
        }
        
        public bool IsValid()
        {
            return Regex.IsMatch(Value, "^(https)://open.spotify.com/track/(.+)([?]si=.*)?");
        }
    }

    public class UrlTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            var url = new Url(input);
            
            if (url.IsValid())
            {
                return Task.FromResult(TypeReaderResult.FromSuccess(url));
            }

            return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed,
                "Tahle URL se mýmu regexu nezdá, ale může se mýlit :D"));
        }
    }
}
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord.Commands;

namespace project_alfred.TypeReaders
{
    public struct Url
    {
        private string _value;
        
        public bool IsValid { private set; get; }

        public Url(string value) : this()
        {
            this.Value = value;
        }
        
        public string Value
        {
            get => _value;
            set
            {
                var match = Regex.Match(value, "^(https)://open.spotify.com/track/(.+)([?]si=.*)?");

                if (match.Success)
                {
                    _value = match.Value;
                }

                IsValid = match.Success;
            }
        }
    }

    public class UrlTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            var url = new Url(input);
            
            if (url.IsValid)
            {
                return Task.FromResult(TypeReaderResult.FromSuccess(url));
            }

            return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed,
                "Tahle URL se mi nezdá, ale můžu se mýlit :D"));
        }
    }
}
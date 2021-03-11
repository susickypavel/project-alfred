using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord.Commands;

namespace project_alfred.TypeReaders
{
    public struct Url
    {
        private string _value;

        public bool IsValid { get; private set; }
        
        public string Value
        {
            get => _value;
            set
            {
                var match = Regex.Match(value, "^(https)://open.spotify.com/track/(.+)([?]si=.*)?");
                
                if (match.Success) {
                    _value = match.Value;
                    IsValid = true;
                }
                else {
                    _value = value;
                    IsValid = false;
                }
            }
        }
        
        public Url(string value) : this()
        {
            IsValid = false;
            Value = value;
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
                "Tahle URL se mýmu regexu nezdá, ale může se mýlit :D"));
        }
    }
}
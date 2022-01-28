using System;
using System.Collections.Generic;
using CommandLine;
using Microsoft.Extensions.Configuration;

namespace BotCli.actions
{
    public interface IAction
    {
        ParserResult<object> Handle(ParserResult<object> parserResult);
    }

    [Verb("talk", HelpText = "Makes the bot say something")]
    public class TalkOptions
    {
        [Option('r', "repeat", Required = false, HelpText = "Repeats the input.")]
        public IEnumerable<string> Args { get; set; }
    }

    public class TalkAction : IAction
    {
        private readonly IConfigurationRoot _configuration;
        public TalkAction(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }
        public ParserResult<object> Handle(ParserResult<object> parserResult)
        {
            parserResult.WithParsed<TalkOptions>(opt =>
            {
                var botname = _configuration["BotName"];
                var pipedText = Utils.CheckPipe();
                var message = pipedText != null  ? "I'm piping: " + pipedText :
                opt.Args == null ? 
                botname + ": Hello World!"
                : $"{botname}: {String.Join(' ', opt.Args)}";
                Console.WriteLine(message);
            });
            
            return parserResult;
        }
    }
}
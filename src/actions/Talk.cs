using System;
using System.Collections.Generic;
using CommandLine;
using Microsoft.Extensions.Configuration;

namespace BotCli.actions
{
    public interface IAction
    {
        int Act();
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
        private readonly Parser _parser;

        public TalkAction(IConfigurationRoot configuration, Parser parser)
        {
            _configuration = configuration;
            _parser = parser;
        }
        public int Act()
        {
            var parserResult = _parser.ParseArguments
                <TalkOptions, ListOptions>(Environment.GetCommandLineArgs());

            parserResult.WithParsed<TalkOptions>(opt =>
            {
                var message = opt.Args == null ? _configuration["BotName"] + ": Hello World!"
                : $"{_configuration["BotName"]} repeats: {String.Join(' ', opt.Args)}";
                Console.WriteLine(message);
            });
            return 0;
        }
    }
}
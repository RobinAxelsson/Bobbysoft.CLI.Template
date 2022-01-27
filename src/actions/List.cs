using System;
using System.Collections.Generic;
using CommandLine;

namespace BotCli.actions
{
    [Verb("list", HelpText = "Lists something.")]
    public class ListOptions
    {
        [Option('g', "go", Required = false, HelpText = "Repeats the input.")]
        public IEnumerable<string> Args { get; set; }
    }
    public class ListAction : IAction
    {
        public ParserResult<object> Handle(ParserResult<object> parserResult){

            parserResult
            .WithParsed<ListOptions>(opt =>
            {
                var message = "list: ho ho ho";
                Console.WriteLine(message);
            });
            return parserResult;
        }
    }

}
using System;
using System.Collections.Generic;
using CommandLine;

namespace BotCli.actions
{
    [Verb("list", HelpText = "Lists encryptions in the default folder.")]
    public class ListOptions
    {
        [Option('r', "repeat", Required = false, HelpText = "Repeats the input.")]
        public IEnumerable<string> Args { get; set; }
    }
    public class ListAction : IAction
    {
        private readonly Parser _parser;

        public ListAction(Parser parser)
        {
            _parser = parser;
        }
        public int Act(){
            var parserResult = _parser.ParseArguments
                <ListOptions, TalkOptions>(Environment.GetCommandLineArgs());

            parserResult.WithParsed<ListOptions>(opt =>
            {
                Console.WriteLine("List: Test monley movie star");
            });
            return 0;
        }
    }

}
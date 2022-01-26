using System;
using CommandLine;

namespace BotCli
{
    class Program
    {
        static int Main(string[] args)
        {
            var app = ConsoleApp.Build(args);
            var config = app.Config;
            
            var parserResult =
            (new Parser()).ParseArguments
            <TalkOptions>(args);
                
            var output = parserResult.MapResult(
            opts => TalkOptions.Run(opts, config),
            errs => 1);

            Console.WriteLine("End of program");
            return output;
        }

    }
}
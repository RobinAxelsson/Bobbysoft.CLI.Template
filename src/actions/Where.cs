using System;
using System.Collections.Generic;
using CommandLine;

namespace BotCli.actions
{
    [Verb("where", HelpText = "Gets path to executable.")]
    public class WhereOptions
    {
      
    }
    public class WhereAction : IAction
    {
        public ParserResult<object> Handle(ParserResult<object> parserResult){

            parserResult
            .WithParsed<WhereOptions>(opt =>
            {
                var path = System.IO.Path
                    .GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)?
                    .Substring(6) ?? "path is null";
                Console.WriteLine(path);
            });
            return parserResult;
        }
    }

}
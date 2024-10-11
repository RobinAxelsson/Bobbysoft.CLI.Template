using System;
using System.Collections.Generic;
using CommandLine;
using TextCopy;

namespace $projectName$.Actions
{
    [Verb("clip", HelpText = "Lists something.")]
    public class ClipOptions
    {
        [Option('i', "input", Required = false, HelpText = "Adds text to clipboard")]
        public IEnumerable<string> Args { get; set; }
    }
    public class ClipAction : IAction
    {
        public ParserResult<object> Handle(ParserResult<object> parserResult){

            parserResult
            .WithParsed<ClipOptions>(opt =>
            {
                if(((string[])opt.Args).Length == 0){
                    var message = ClipboardService.GetText();
                    Console.WriteLine(message);
                }
                else {
                    ClipboardService.SetText(String.Join(' ', opt.Args));
                    Console.WriteLine("Input copied");
                }
            });
            return parserResult;
        }
    }

}
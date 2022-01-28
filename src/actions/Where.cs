using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;

namespace BotCli.actions
{
    [Verb("where", HelpText = "Gets path to executable.")]
    public class WhereOptions
    {
        [Option('n', "name", Required = false, HelpText = "Finds the enumerated folder paths by name.")]
        public string Name {get; set;}
    }
    public class WhereAction : IAction
    {
        public ParserResult<object> Handle(ParserResult<object> parserResult){

            parserResult
            .WithParsed<WhereOptions>(opt =>
            {

                var path = opt.Name == null ?
                    System.IO.Path
                    .GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)?
                    .Substring(6) ?? "path is null" :
                    ToPath(opt.Name.ToLower());
                Console.WriteLine(path);
            });
            return parserResult;
        }

        static string ToPath(string name) => name switch
        {

        "userprofile" => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        "temp" => Path.GetTempPath(),
        "desktop" => Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
        "programfiles" => Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
        "system" => Environment.GetFolderPath(Environment.SpecialFolder.System),
        _ => "Not found",
        };
    }
    
    

}
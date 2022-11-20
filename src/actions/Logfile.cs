using Serilog.Core;
using SwDb.CLI.ActionDefintions;
using SwDb.CLI.Utils;
using System.Diagnostics;

namespace SwDb.CLI.CliModules;

[Verb("logfile", HelpText = "Opens the appsettings file in default editor")]
public class LogfileOptions : IVerbOptions { }
public class LogfileAction : IAction
{
    private readonly Logger _logger;
    public LogfileAction(Logger logger)
    {
        _logger = logger;
    }
    public ParserResult<object> Handle(ParserResult<object> parserResult)
    {
        parserResult.WithParsed((Action<LogfileOptions>)(opt =>
        {
            var path = PathUtil.LogFile;
            if (new FileInfo(path).Exists == false)
                File.AppendAllText(path, "");
            Console.WriteLine("Opening log.txt");
            new Process() { StartInfo = new ProcessStartInfo(path) { UseShellExecute = true } }.Start();
            Environment.Exit(0);
        }));

        return parserResult;
    }
}

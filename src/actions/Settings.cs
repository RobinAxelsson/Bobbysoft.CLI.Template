using Serilog.Core;
using SwDb.CLI.ActionDefintions;
using SwDb.CLI.Utils;
using System.Diagnostics;

namespace SwDb.CLI.CliModules;

[Verb("settings", HelpText = "Opens the appsettings file in default editor")]
public class SettingsOptions : IVerbOptions { }

public class SettingsAction : IAction
{
    private readonly Logger _logger;
    public SettingsAction(Logger logger)
    {
        _logger = logger;
    }
    public ParserResult<object> Handle(ParserResult<object> parserResult)
    {
        parserResult.WithParsed((Action<SettingsOptions>)(opt =>
        {
            var path = PathUtil.AppSettings;
            if (new FileInfo(path).Exists == false)
            {
                Console.WriteLine("appsettings.json is missing consider reinstalling the app");
                Environment.Exit(1);
            }
            Console.WriteLine("Opening settings file");
            new Process() { StartInfo = new ProcessStartInfo(path) { UseShellExecute = true } }.Start();
            Environment.Exit(0);
        }));

        return parserResult;
    }
}

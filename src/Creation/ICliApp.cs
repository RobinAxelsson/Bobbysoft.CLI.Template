using SwDb.CLI.ActionDefintions;

namespace SwDb.CLI.Creation;

internal interface ICliApp
{
    ICliApp HandleAllActions();
    ICliApp Handle<T>() where T : IAction;
    ICliApp ParseArguments(string[] args);
    IConfigurationRoot Config { get; }
}

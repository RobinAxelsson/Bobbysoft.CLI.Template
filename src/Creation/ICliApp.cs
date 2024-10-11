using $projectName$.ActionDefintions;

namespace $projectName$.Creation;

internal interface ICliApp
{
    ICliApp HandleAllActions();
    ICliApp Handle<T>() where T : IAction;
    ICliApp ParseArguments(string[] args);
    IConfigurationRoot Config { get; }
}

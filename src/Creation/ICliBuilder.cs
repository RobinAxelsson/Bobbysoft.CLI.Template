namespace $projectName$.Creation;

internal interface ICliBuilder
{
    ICliBuilder AddCliActions();
    ICliBuilder AddConfigurations(Func<IConfigurationBuilder, IConfigurationBuilder> configure);
    ICliBuilder AddServices(Action<IServiceCollection> addServices);
    ICliBuilder AddParser(Parser parser);
    ICliApp Build();
}

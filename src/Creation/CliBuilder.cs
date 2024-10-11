using $projectName$.ActionDefintions;

namespace $projectName$.Creation;

internal class CliBuilder : ICliBuilder
{
    private IServiceCollection _services = new ServiceCollection();
    private static CliBuilder _builder = new();
    public static CliBuilder Default { get => _builder; }
    private CliBuilder() { }
    public ICliBuilder AddConfigurations(Func<IConfigurationBuilder, IConfigurationBuilder> configure)
    {
        var config = new ConfigurationBuilder();
        configure(config);
        var root = config.Build();
        _services.AddSingleton(root);
        return this;
    }
    public ICliBuilder AddServices(Action<IServiceCollection> addServices)
    {
        addServices(_services);
        return this;
    }
    public ICliBuilder AddParser(Parser parser)
    {
        _services.AddSingleton(parser);
        return this;
    }
    public ICliBuilder AddCliActions()
    {
        var actionTypes = Assembly.GetEntryAssembly().GetTypes()
            .Where(t => typeof(IAction).IsAssignableFrom(t) && !t.IsInterface).ToList();

        actionTypes.ForEach(t => _services.AddSingleton(typeof(IAction), t));
        return this;
    }
    public ICliApp Build()
    {
        var provider = _services.BuildServiceProvider();
        return new CliApp(provider);
    }

}

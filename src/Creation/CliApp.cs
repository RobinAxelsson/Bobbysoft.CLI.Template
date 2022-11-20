using SwDb.CLI.ActionDefintions;

namespace SwDb.CLI.Creation
{
    internal class CliApp : ICliApp
    {
        private ParserResult<object> _parserResult;
        private IServiceProvider _provider;
        public IConfigurationRoot Config { get => _provider.GetService<IConfigurationRoot>(); }
        public CliApp(IServiceProvider provider)
        {
            _provider = provider;
        }
        public ICliApp ParseArguments(string[] args)
        {
            var parser = _provider.GetService<Parser>();
            if (parser is null) throw new ArgumentNullException(nameof(Parser));

            var verbOptionTypes = Assembly.GetEntryAssembly().GetTypes()
                .Where(t => typeof(IVerbOptions).IsAssignableFrom(t) && !t.IsInterface);

            _parserResult = parser.ParseArguments(args, verbOptionTypes.ToArray());
            return this;
        }
        public ICliApp HandleAllActions()
        {
            var actions = _provider.GetServices<IAction>();

            if (actions is null || actions.Count() == 0)
                throw new ArgumentNullException(nameof(IEnumerable<IAction>));

            if (_parserResult is null)
                throw new ArgumentNullException(nameof(_parserResult));

            var _ = actions.ToList()
                .Aggregate(_parserResult, (result, action)
                    => action.Handle(result));
            return this;
        }

        public ICliApp Handle<T>() where T : IAction
        {
            if (_parserResult is null) throw new ArgumentNullException(nameof(_parserResult));
            var action = _provider.GetServices<IAction>()?
                .FirstOrDefault(a => a.GetType() == typeof(T));

            action.Handle(_parserResult);
            return this;
        }
    }
}

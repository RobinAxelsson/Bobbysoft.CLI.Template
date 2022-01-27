using System.Linq;
using BotCli.actions;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotCli
{
    public class ConsoleApp
    {
        private ServiceProvider _provider;
        private ConsoleApp(ServiceProvider provider)
        {
            _provider = provider;
        }
        public static ConsoleApp Build()
        {
            var serviceCollection = new ServiceCollection();
            
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables()
                .Build();
                    
            serviceCollection.AddSingleton<IConfigurationRoot>(config);
            serviceCollection.AddSingleton<IAction, TalkAction>();
            serviceCollection.AddSingleton<IAction, ListAction>();

            var provider = serviceCollection.BuildServiceProvider();

            return new ConsoleApp(provider);
        }
        public void Run(string[] args)
        {
            //Add all the options here
            var parserResult = Parser.Default.ParseArguments<TalkOptions, ListOptions>(args);
            
            //Gets registered actions and runs all the handles and passes the result to the next (like a middleware)
            var actions = _provider.GetServices<IAction>();
            actions.ToList().Aggregate(parserResult, (result, action) => action.Handle(result));
        }
    }
}
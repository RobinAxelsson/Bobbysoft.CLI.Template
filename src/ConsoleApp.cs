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
                .AddUserSecrets<Program>() //User secrets commands our found in README.md
                .AddEnvironmentVariables()
                .Build();
                    
            serviceCollection.AddSingleton<IConfigurationRoot>(config);
            serviceCollection.AddSingleton<Parser>();
            serviceCollection.AddSingleton<Runner>();

            var provider = serviceCollection.BuildServiceProvider();

            return new ConsoleApp(provider);
        }
        public void Run(){
            _provider.GetService<Runner>().ExecuteProgram();
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotCli
{
    public class ConsoleApp
    {
        private ServiceProvider _provider;
        public IConfigurationRoot Config { get; private set; }
        public T GetService<T>() => _provider.GetService<T>();
        private ConsoleApp(ServiceProvider provider)
        {
            _provider = provider;
            Config = _provider.GetService<IConfigurationRoot>();
        }
        public static ConsoleApp Build(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>() //User secrets commands our found in README.md
                .AddEnvironmentVariables()
                .Build();
                    
            serviceCollection.AddSingleton<IConfigurationRoot>(config);
            // serviceCollection.AddSingleton<IConfigurationRoot>(config);

            var provider = serviceCollection.BuildServiceProvider();
            return new ConsoleApp(provider);
        }
    }
}
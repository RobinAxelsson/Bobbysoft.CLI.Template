﻿using System;
using System.Linq;
using BotCli.actions;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace $projectName$
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
            serviceCollection.AddSingleton<IAction, WhereAction>();
            serviceCollection.AddSingleton<IAction, ClipAction>();

            var provider = serviceCollection.BuildServiceProvider();

            return new ConsoleApp(provider);
        }
        public void Run(string[] args)
        {
            //Add all the options here
            var parserResult = Parser.Default.ParseArguments<TalkOptions, WhereOptions, ClipOptions>(args);
            
            //Gets registered actions
            var actions = _provider.GetServices<IAction>();

            //Runs all the actions in sequence
            actions.ToList().Aggregate(parserResult, (result, action) => action.Handle(result));
        }
    }
}
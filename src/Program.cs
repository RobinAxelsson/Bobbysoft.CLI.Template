using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using CommandLine;
using System.Linq;

namespace VarbyteCli
{
    class Program
    {
        static void Main(string[] args)
        {
            //Loads all variables from multiple sources, overrides if reoccurrence.
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>() //User secrets commands our found in README.md
                .AddEnvironmentVariables()
                .Build();

            CommandLine.Parser.Default.ParseArguments
                <MakeOptions,
                ListOptions,
                DecryptOptions,
                DeleteOptions,
                CreateKeyOptions,
                PathOptions>(args);

            Console.WriteLine("End of program");
        }
    }
}
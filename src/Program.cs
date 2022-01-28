using System;
using CommandLine;

namespace BotCli
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleApp
            .Build()
            .Run(args);
        }
    }
}
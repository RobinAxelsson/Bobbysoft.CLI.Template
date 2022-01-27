using System;
using CommandLine;

namespace BotCli
{
    class Program
    {
        static void Main()
        {
            ConsoleApp
            .Build()
            .Run();
            Console.WriteLine("End of program");
        }
    }
}
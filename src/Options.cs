using System;
using System.Collections.Generic;
using CommandLine;
using Microsoft.Extensions.Configuration;

//https://github.com/commandlineparser/commandline#command-line-parser-library-for-clr-and-netstandard

namespace BotCli
{
    //You allways need a verb!
    [Verb("talk", HelpText = "Makes the bot say something")]
    class TalkOptions
    {
        [Option('r', "repeat", Required = false, HelpText = "Repeats the input.")]
        public IEnumerable<string> Args { get; set; }

        public static int Run(TalkOptions opts, IConfigurationRoot config)
        {
            var message = opts.Args == null ? config["BotName"] + ": Hello World!" 
                : $"{config["BotName"]} repeats: {String.Join(' ', opts.Args)}";
            Console.WriteLine(message);
            return 0;
        }
    }

    [Verb("list", HelpText = "Lists encryptions in the default folder.")]
    class ListOptions
    {
        public IEnumerable<string> Args { get; set; }
    }
    [Verb("path", HelpText = "Handles encryptions path.")]
    class PathOptions
    {
        [Option('s', "set", HelpText = "Sets the path to the encryptions")]
        public string NewPath { get; set; }
        [Option('g', "get", HelpText = "Prints path and sends to clipboard")]
        public IEnumerable<string> Args { get; set; }
    }
    [Verb("make", HelpText = "Adds a new encryption to configurated encryption folder")]
    class MakeOptions
    {
        [Option('c', "from clipboard", HelpText = "Encrypts whats in the clipboard")]
        public IEnumerable<string> ClipArgs { get; set; }
        [Option('p', "file path", HelpText = "Input uri to be encrypted.")]
        public string FilePath { get; set; }
        [Option('o', "output", HelpText = "Output file path")]
        public string OutputPath { get; set; }
        [Option('i', "std-in", HelpText = "Type to std-in concealed")]
        public IEnumerable<string> StdArgs { get; set; }
    }
    [Verb("decrypt", HelpText = "Decrypts encrypted data")]
    class DecryptOptions
    {
        [Option('c', "to clipboard", HelpText = "Decrypts and copies to the clipboard")]
        public IEnumerable<string> ClipArgs { get; set; }
        [Option('f', "to file", HelpText = "Output uri for decrypted file.")]
        public string FilePath { get; set; }
        [Option('s', "standard out", HelpText = "Prints output to std out")]
        public IEnumerable<string> StdArgs { get; set; }
    }
    [Verb("delete", HelpText = "Deletes encryptions.")]
    class DeleteOptions
    {
        [Option('n', "name", Required = true, HelpText = "Encryption file-name to delete.")]
        public string Name { get; set; }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotCli
{
    public class Runner
    {
        private readonly IConfigurationRoot _configuration;
        private readonly Parser _parser;

        public Runner(IConfigurationRoot configuration, Parser parser)
        {
            _configuration = configuration;
            _parser = parser;
        }
        public void ExecuteProgram()
        {

            var parserResult = _parser.ParseArguments
            <TalkOptions>(Environment.GetCommandLineArgs());

            parserResult.WithParsed<TalkOptions>(opt => opt.Try(_configuration));
        }
    }
}
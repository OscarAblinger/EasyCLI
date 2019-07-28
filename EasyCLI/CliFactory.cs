using EasyCli.Impl;
using System;
using System.Collections.Generic;

namespace EasyCli
{
    public static class CliFactory
    {
        public static ICli Create()
        {
            return new Cli(CreateDefaultConfig());
        }

        public static ICli CreateWithDefaults(
                OneOf<string, string[], Action<ICli>> greeting = null,
                Either<string, Action<ICli>> prompt = null,
                ExceptionCatchHandler exceptionCatchHandler = null,
                IList<ICommand> commands = null
            )
        {
            return new Cli(
                CreateConfigWithDefaults(greeting, prompt, exceptionCatchHandler, commands)
                );
        }

        public static IConfiguration CreateDefaultConfig()
        {
            return new Configuration();
        }

        public static IConfiguration CreateConfigWithDefaults(
                OneOf<string, string[], Action<ICli>> greeting = null,
                Either<string, Action<ICli>> prompt = null,
                ExceptionCatchHandler exceptionCatchHandler = null,
                IList<ICommand> commands = null
            )
        {
            return new Configuration()
            {
                IOImplementation = new ConsoleIO(),
                // TODO: check commands for 'help' command and add
                //   notice if it's available
                Greeting = greeting ?? "Hello There!",
                Prompt = prompt ?? "> ",
                ExceptionCatchHandler = exceptionCatchHandler ?? ((cli, ex) =>
                {
                    cli.Out.WriteLine($"An error occured: {ex.GetType().Name}: {ex.Message}");
                    cli.Out.WriteLine(ex.StackTrace);
                }),
                Commands = commands ?? new List<ICommand>()
            };
        }
    }
}

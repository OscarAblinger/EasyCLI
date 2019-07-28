using System;
using System.Collections.Generic;
using EasyCli.ConsoleInterface;

namespace EasyCli.Impl
{
    internal class Configuration : IConfiguration
    {
        public IConsoleIO IOImplementation { get; set; }
        public OneOf<string, string[], Action<ICli>> Greeting { get; set; }
        public Either<string, Action<ICli>> Prompt { get; set; }
        public ExceptionCatchHandler ExceptionCatchHandler { get; set; }
        public IList<ICommand> Commands { get; set; }
    }
}

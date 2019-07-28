using EasyCli.ConsoleInterface;
using System;
using System.Collections.Generic;

namespace EasyCli
{
    public delegate void ExceptionCatchHandler(Exception ex);

    public interface IConfiguration
    {
        IConsoleIO IOImplementation { get; set; }

        Either<string, Action<ICli>> Greeting { get; set; }
        Either<string, Action<ICli>> Prompt { get; set; }

        ExceptionCatchHandler ExceptionCatchHandler { get; set; }
        IList<ICommand> Commands { get; set; }
    }
}
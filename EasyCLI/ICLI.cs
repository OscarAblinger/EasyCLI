using System;
using System.Collections.Generic;

namespace EasyCli
{
    public interface ICli
    {
        #region Setup
        /// <summary>
        /// Starts the CLI (blocks until CLI is exited)
        /// </summary>
        void Run();

        /// <summary>
        /// Loads the config
        /// </summary>
        /// <param name="configuration">Configuration object to load</param>
        void LoadConfiguration(IConfiguration configuration);

        #region Register Command Overloads
        /// <summary>
        /// Registers a new Command
        /// </summary>
        /// <param name="command">The command to register</param>
        /// <returns>The original CLI for chaining commands</returns>
        ICli RegisterCommand(ICommand command);
        /// <summary>
        /// Registers multiple new Commands at once
        /// </summary>
        /// <param name="commands">The commands to register</param>
        /// <returns>The original CLI for chaining commands</returns>
        ICli RegisterCommands(IEnumerable<ICommand> commands);
        /// <summary>
        /// Registers a new Command
        /// </summary>
        /// <param name="name">The name by which the command can be invoked</param>
        /// <param name="method">The method that will be invoked</param>
        /// <param name="description">Description of the Command used in the help Command</param>
        /// <returns>The original CLI for chaining commands</returns>
        ICli RegisterCommand(string name, CommandMethod method, string description);
        /// <summary>
        /// Registers a new Command
        /// </summary>
        /// <param name="names">A list of names by which the command can be invoked</param>
        /// <param name="method">The method that will be invoked</param>
        /// <param name="description">Description of the Command used in the help Command</param>
        /// <returns>The original CLI for chaining commands</returns>
        ICli RegisterCommand(string[] names, CommandMethod method, string description);
        /// <summary>
        /// Registers a new Command
        /// </summary>
        /// <param name="name">The name by which the command can be invoked</param>
        /// <param name="method">The method that will be invoked</param>
        /// <param name="description">
        ///     Description of the Command used in the help Command.
        ///     Each string gets printed on a separate line
        /// </param>
        /// <returns>The original CLI for chaining commands</returns>
        ICli RegisterCommand(string name, CommandMethod method, string[] description = null);
        /// <summary>
        /// Registers a new Command
        /// </summary>
        /// <param name="names">A list of names by which the command can be invoked</param>
        /// <param name="method">The method that will be invoked</param>
        /// <param name="description">
        ///     Description of the Command used in the help Command.
        ///     Each string gets printed on a separate line
        /// </param>
        /// <returns>The original CLI for chaining commands</returns>
        ICli RegisterCommand(string[] names, CommandMethod method, string[] description = null);
        #endregion
        #endregion

        #region Interaction Properties and Commands
        /// <summary>
        /// The current list of all commands
        /// </summary>
        ICommand[] GetCommands();

        /// <summary>
        /// Wether or not the CLI is targeting a Console element.
        /// Use this to check wether advanced options (like colored output) is possible
        /// </summary>
        /// <returns>True when a Console object is available</returns>
        bool IsPrintingToConsole();

        /// <summary>
        /// Runs a command
        /// </summary>
        /// <param name="command">The command line that should be executed</param>
        ICommandResult RunCommand(string command);

        /// <summary>
        /// Suspends the cli until the continue command was invoked
        /// </summary>
        /// <returns>The command that has to be invoked to lift the suspense</returns>
        Action WaitUntil();
        #endregion
    }
}

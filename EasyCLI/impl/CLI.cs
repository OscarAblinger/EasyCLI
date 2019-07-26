using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyCli.impl
{
    internal class Cli : ICli
    {
        #region Public Interface
        public void Run() => Console.ReadKey();

        public void LoadConfiguration(IConfiguration configuration) => throw new NotImplementedException();

        public ICli RegisterCommand(ICommand command)
        {
            AssertValidCommand(command);
            SaveCommandInList(command);
            return this;
        }

        public ICli RegisterCommands(IEnumerable<ICommand> commands)
        {
            foreach(var command in commands)
            {
                RegisterCommand(command);
            }
            return this;
        }

        public ICli RegisterCommand(string name, CommandMethod method, string description = "")
            => RegisterCommand(new string[] { name }, method, new string[] { description });

        public ICli RegisterCommand(string[] names, CommandMethod method, string description)
            => RegisterCommand(names, method, new string[] { description });

        public ICli RegisterCommand(string name, CommandMethod method, string[] description = null)
            => RegisterCommand(new string[] { name }, method, description);

        public ICli RegisterCommand(string[] names, CommandMethod method, string[] description = null)
            => RegisterCommand(new Command(names, method, description ?? new string[0]));

        public bool IsPrintingToConsole() => throw new NotImplementedException();

        public ICommandResult RunCommand(string command)
        {
            if (!Commands.ContainsKey(command))
            {
                throw new CommandNotFoundException();
            }

            return RunCommand(Commands[command], command);
        }

        public ICommandResult RunCommand(ICommand command, string arguments)
        {
            return command.Method(this, new ArgumentsInfo(arguments));
        }
       
        public Action WaitUntil() => throw new NotImplementedException();

        public ICommand[] GetCommands()
        {
            return Commands.Values.ToArray();
        }
        #endregion

        #region Private
        private Dictionary<string, ICommand> Commands { get; set; }
        private IConfiguration config;

        private void AssertValidCommand(ICommand command)
        {
            if (command.Names == null || command.Names.Length == 0)
                throw new ArgumentException("Cannot register command: No names provided");
            if (command.Names.Any(name => string.IsNullOrWhiteSpace(name)))
                throw new ArgumentException("No name may be null or whitespace");
            if (command.Method == null)
                throw new ArgumentException("Cannot register command: No method provided");
            if (command.Description == null)
                throw new ArgumentException("Cannot register command: Description shouldn't be null (use an empty array instead)");
            if (command.Description.Any(des => des == null))
                throw new ArgumentException("No description entry may be null");
        }

        private void SaveCommandInList(ICommand command)
        {
            foreach(var name in command.Names)
            {
                Commands[name] = command;
            }
        }

        internal Cli(IConfiguration config) {
            Commands = new Dictionary<string, ICommand>();
            this.config = config;
        }
        #endregion
    }
}

﻿using System;
using System.Collections.Generic;

namespace EasyCli
{
    internal class Cli: ICli
    {
        #region Public Interface
        public void Run()
        {
            Console.ReadKey();
        }

        public void LoadConfiguration(IConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public ICli RegisterCommand(ICommand command)
        {
            Commands.Add(command);
            CommandTree.Add(command);
        }

        public ICli RegisterCommand(string name, CommandMethod method, string description = "")
            => this.RegisterCommand(new string[] { name }, method, new string[] { description });

        public ICli RegisterCommand(string name, CommandMethod method, string[] description = null)
            => this.RegisterCommand(new string[] { name }, method, description);

        public ICli RegisterCommand(string[] names, CommandMethod method, string[] description = null)
        {
            throw new NotImplementedException();
        }

        public bool IsPrintingToConsole()
        {
            throw new NotImplementedException();
        }

        public ICommandResult RunCommand(string command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult RunCommand(ICommand command)
        {
            throw new NotImplementedException();
        }

        public void WaitUntil(Action continueCommand)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private
        private List<ICommand> Commands { get; set; }
        private CommandNode CommandTree { get; set; }

        internal Cli() { }
        #endregion
    }
}

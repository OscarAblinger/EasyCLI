using System;
using System.Collections.Generic;
using System.Text;

namespace EasyCli.impl
{
    internal class Command : ICommand
    {
        public string[] Names { get; }
        public CommandMethod Method { get; }
        public string[] Description { get; }

        internal Command(string[] names, CommandMethod method, string[] description)
        {
            Names = names;
            Method = method;
            Description = description;
        }
    }
}

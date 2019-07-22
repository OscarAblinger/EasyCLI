using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EasyCli
{
    internal class CommandNode : ICommandNode
    {
        public int Count { get; private set; }
        public bool IsReadOnly => false;
        public bool IsSynchronized => false;

        public object SyncRoot => new object();

        private IDictionary<char, CommandNode> Children = new SortedDictionary<char, CommandNode>();
        private ICommand Command;

        public void Add(ICommand item)
        {
            AddToTree(item, item.Names.Where(name => !string.IsNullOrWhiteSpace(name)), 0);
            UpdateCount();
        }

        private void AddToTree(ICommand item, IEnumerable<string> names, int currentDepth)
        {
            foreach(var name in names)
            {
                if (name.Length == currentDepth)
                {
                    if (Command != null)
                        throw new ArgumentException("A command with the same name has already been added");
                    Command = item;
                }
            }

            var possibleNames = names.Where(name =>
                name.Length > currentDepth);

            foreach (var name in possibleNames)
            {
                if (!Children.ContainsKey(name[currentDepth]))
                    Children.Add(name[currentDepth], new CommandNode());
            }

            foreach (var childKvp in Children)
            {
                var possibleChildNames = possibleNames.Where(name =>
                    name[currentDepth] == childKvp.Key
                );
                childKvp.Value.AddToTree(item, possibleChildNames, currentDepth + 1);
            }
        }

        public bool Remove(ICommand item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Children.Clear();
            Count = 0;
        }

        public bool Contains(ICommand item)
        {
            return Contains(item, item.Names, 0);
        }

        private bool Contains(ICommand item, IEnumerable<string> names, int currentDepth)
        {
            if (!names.Any())
                return false;
            else if (Command == item)
                return true;
            else
                return Children.Any(childKvp =>
                {
                    var possibleNames = names.Where(name =>
                        name.Length > currentDepth &&
                        name[currentDepth] == childKvp.Key
                    );
                    return childKvp.Value.Contains(item, possibleNames, currentDepth + 1);
                });
        }

        public void CopyTo(ICommand[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), arrayIndex, "Index must not be negative");
            if (arrayIndex > array.Length || Count > array.Length - arrayIndex)
                throw new ArgumentException("Array too small");

            foreach(var command in this)
            {
                array[arrayIndex] = command;
                ++arrayIndex;
            }
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo(array as ICommand[], index);
        }

        public IEnumerator<ICommand> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
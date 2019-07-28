using EasyCli.ConsoleInterface;
using System;
using System.Text;

namespace EasyCli.Impl
{
    internal class ConsoleIn : IInput
    {
        public event OnAutocompleteRequestEventHandler OnAutocompleteRequested;

        public void Dispose() { }

        public string ReadLine()
        {
            StringBuilder sb = new StringBuilder();
            ConsoleKey key;
            do
            {
                key = ReadKeyIntoStringBuilder(sb);
            } while (key != ConsoleKey.Enter);

            return sb.ToString();
        }

        private ConsoleKey ReadKeyIntoStringBuilder(StringBuilder sb)
        {
            var keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Tab)
            {
                InvokeAutocomplete(sb);
            }

            sb.Append(keyInfo.KeyChar);
            return keyInfo.Key;
        }

        private void InvokeAutocomplete(StringBuilder sb)
        {
            OnAutocompleteRequested.Invoke(
                this,
                new OnAutocompleteRequestEventArgs()
                {
                    LineUpToCursor = sb.ToString()
                });
        }
    }
}
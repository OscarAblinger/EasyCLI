using System;

namespace EasyCli.ConsoleInterface
{
    public delegate void OnAutocompleteRequestEventHandler(IInput source, OnAutocompleteRequestEventArgs args);

    public interface IInput : IDisposable
    {
        /// <summary>
        /// Reads a line of characters and returns the data as a string
        /// </summary>
        /// <returns>The next line from the reader, or null if all characters have been read.</returns>
        string ReadLine();

        /// <summary>
        /// Event that should get thrown if the user wants to autocomplete
        /// </summary>
        event OnAutocompleteRequestEventHandler OnAutocompleteRequested;
    }
}

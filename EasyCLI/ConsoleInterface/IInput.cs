using System;

namespace EasyCli.ConsoleInterface
{
    public delegate void OnAutocompleteRequestEventHandler(IInput source, OnAutocompleteRequestEventArgs args)

    public interface IInput : IDisposable
    {
        /// <summary>
        /// Obains the next character or function pressed by the user.
        /// The pressed key is optionally written to the output
        /// </summary>
        /// <param name="noDisplay">
        /// If True will not display the pressed key, otherwise it will
        /// </param>
        /// <returns>Information about the pressed key</returns>
        ConsoleKeyInfo ReadKey(bool noDisplay = false);

        /// <summary>
        /// Event that should get thrown if the user wants to autocomplete
        /// </summary>
        event OnAutocompleteRequestEventHandler OnAutocompleteRequested;
    }
}

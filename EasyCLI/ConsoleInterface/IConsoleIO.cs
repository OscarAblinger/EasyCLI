namespace EasyCli.ConsoleInterface
{
    public interface IConsoleIO
    {
        IInput In { get; }
        IOutput Out { get; }
        ICursor Cursor { get; }

        /// <summary>
        /// Remove a number of already printed characters.
        /// 
        /// This method may do nothing
        /// </summary>
        /// <param name="number"></param>
        void DeleteCharacters(int number);

        /// <summary>
        /// Gets the width of the buffer area.
        /// Setter not available as some OS don't offer support for that
        /// </summary>
        int BufferHeight { get; }
        /// <summary>
        /// Gets the height of the buffer area.
        /// Setter not available as some OS don't offer support for that
        /// </summary>
        int BufferWidth { get; }
    }
}

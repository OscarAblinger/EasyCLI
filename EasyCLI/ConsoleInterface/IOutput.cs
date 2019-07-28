using System;

namespace EasyCli.ConsoleInterface
{
    public enum OutputColor
    {
        Black, DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta,
        DarkYellow, Gray, DarkGray, Blue, Green, Cyan, Red, Magenta,
        Yellow, White
    }

    public static class OutputColorExtensions
    {
        public static ConsoleColor ToConsoleColor(this OutputColor color)
        {
            switch (color)
            {
                case OutputColor.Black:
                    return ConsoleColor.Black;
                case OutputColor.DarkBlue:
                    return ConsoleColor.DarkBlue;
                case OutputColor.DarkGreen:
                    return ConsoleColor.DarkGreen;
                case OutputColor.DarkCyan:
                    return ConsoleColor.DarkCyan;
                case OutputColor.DarkRed:
                    return ConsoleColor.DarkRed;
                case OutputColor.DarkMagenta:
                    return ConsoleColor.DarkMagenta;
                case OutputColor.DarkYellow:
                    return ConsoleColor.DarkYellow;
                case OutputColor.Gray:
                    return ConsoleColor.Gray;
                case OutputColor.DarkGray:
                    return ConsoleColor.DarkGray;
                case OutputColor.Blue:
                    return ConsoleColor.Blue;
                case OutputColor.Green:
                    return ConsoleColor.Green;
                case OutputColor.Cyan:
                    return ConsoleColor.Cyan;
                case OutputColor.Red:
                    return ConsoleColor.Red;
                case OutputColor.Magenta:
                    return ConsoleColor.Magenta;
                case OutputColor.Yellow:
                    return ConsoleColor.Yellow;
                case OutputColor.White:
                    return ConsoleColor.White;
                default:
                    return ConsoleColor.White;
            }
        }
    }

    public interface IOutput : IDisposable
    {

        #region Meta Information and advanced features
        /// <summary>
        /// Gets or sets the line terminator string used by the current TextWriter
        /// </summary>
        string NewLine { get; set; }

        /// <summary>
        /// Sets font color of the display.
        /// 
        /// This property may do nothing
        /// </summary>
        OutputColor ForegrondColor { get; set; }
        /// <summary>
        /// Sets background color of the display.
        /// 
        /// This property may do nothing
        /// </summary>
        OutputColor BackgroundColor { get; set; }
        /// <summary>
        /// Resets the background and foreground colors back to their defaults.
        /// 
        /// If Foreground and/or BackgroundColor are considered, this may not do nothing
        /// </summary>
        void ResetColor();
        #endregion

        #region Writting functions
        /// <summary>
        /// Clears the buffer for the current writer and causes any buffered data
        /// to be written to the underlying device
        /// </summary>
        void Flush();

        #region Write
        void Write(char value);
        void Write(string value);
        void Write(string format, params object[] arg);
        void Write(int value);
        void Write(long value);
        void Write(float value);
        void Write(double value);
        void Write(decimal value);
        void Write(char[] buffer, int index, int count);
        void Write(char[] buffer);
        void Write(bool value);
        void Write(object value);
        #endregion

        #region WriteLine
        void WriteLine();
        void WriteLine(char value);
        void WriteLine(string value);
        void WriteLine(string format, params object[] arg);
        void WriteLine(int value);
        void WriteLine(long value);
        void WriteLine(float value);
        void WriteLine(double value);
        void WriteLine(decimal value);
        void WriteLine(char[] buffer, int index, int count);
        void WriteLine(char[] buffer);
        void WriteLine(bool value);
        void WriteLine(object value);
        #endregion
        #endregion
    }
}

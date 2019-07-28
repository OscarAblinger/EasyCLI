using EasyCli.ConsoleInterface;
using System;

namespace EasyCli.Impl
{
    internal class ConsoleIO : IConsoleIO
    {
        public IInput In { get; } = new ConsoleIn();
        public IOutput Out { get; } = new ConsoleOut();

        public ICursor Cursor { get; }

        public int BufferHeight { get => Console.BufferHeight; }
        public int BufferWidth { get => Console.BufferWidth; }

        public void Clear()
        {
            Console.Clear();
        }

        public void DeleteCharacters(int number)
        {
            int actualN = GetLimitedN(number);

            PutCursorBackBy(actualN);
            ClearNextNSpaces(actualN);
            PutCursorBackBy(actualN);
        }

        private int GetLimitedN(int number)
        {
            return Math.Min(Cursor.CursorLeft, number);
        }

        private void PutCursorBackBy(int columnsAmount)
        {
            Cursor.CursorLeft -= columnsAmount;
        }

        private void ClearNextNSpaces(int n)
        {
            Out.Write(new string(' ', n));
        }
    }
}

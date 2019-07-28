using EasyCli.ConsoleInterface;
using System;

namespace EasyCli.Impl
{
    internal class ConsoleOut : IOutput
    {
        public string NewLine {
            get => Console.Out.NewLine;
            set => Console.Out.NewLine = value;
        }
        public OutputColor ForegrondColor {
            get => Console.ForegroundColor.ToOutputColor();
            set => Console.ForegroundColor = value.ToConsoleColor();
        }
        public OutputColor BackgroundColor {
            get => Console.BackgroundColor.ToOutputColor();
            set => Console.BackgroundColor = value.ToConsoleColor();
        }

        public void Dispose() { }

        public void Flush()
        {
            Console.Out.Flush();
        }

        public void ResetColor()
        {
            Console.ResetColor();
        }

        public void Write(char value)
        {
            Console.Out.Write(value);
        }

        public void Write(string value)
        {
            Console.Out.Write(value);
        }

        public void Write(string format, params object[] arg)
        {
            Console.Out.Write(format, arg);
        }

        public void Write(int value)
        {
            Console.Out.Write(value);
        }

        public void Write(long value)
        {
            Console.Out.Write(value);
        }

        public void Write(float value)
        {
            Console.Out.Write(value);
        }

        public void Write(double value)
        {
            Console.Out.Write(value);
        }

        public void Write(decimal value)
        {
            Console.Out.Write(value);
        }

        public void Write(char[] buffer, int index, int count)
        {
            Console.Out.Write(buffer, index, count);
        }

        public void Write(char[] buffer)
        {
            Console.Out.Write(buffer);
        }

        public void Write(bool value)
        {
            Console.Out.Write(value);
        }

        public void Write(object value)
        {
            Console.Out.Write(value);
        }

        public void WriteLine()
        {
            Console.Out.WriteLine();
        }

        public void WriteLine(char value)
        {
            Console.Out.WriteLine(value);
        }

        public void WriteLine(string value)
        {
            Console.Out.WriteLine(value);
        }

        public void WriteLine(string format, params object[] arg)
        {
            Console.Out.WriteLine(format, arg);
        }

        public void WriteLine(int value)
        {
            Console.Out.WriteLine(value);
        }

        public void WriteLine(long value)
        {
            Console.Out.WriteLine(value);
        }

        public void WriteLine(float value)
        {
            Console.Out.WriteLine(value);
        }

        public void WriteLine(double value)
        {
            Console.Out.WriteLine(value);
        }

        public void WriteLine(decimal value)
        {
            Console.Out.WriteLine(value);
        }

        public void WriteLine(char[] buffer, int index, int count)
        {
            Console.Out.WriteLine(buffer, index, count);
        }

        public void WriteLine(char[] buffer)
        {
            Console.Out.WriteLine(buffer);
        }

        public void WriteLine(bool value)
        {
            Console.Out.WriteLine(value);
        }

        public void WriteLine(object value)
        {
            Console.Out.WriteLine(value);
        }
    }
}
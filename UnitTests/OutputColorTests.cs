using EasyCli.ConsoleInterface;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class OutputColorTests
    {
        public static IEnumerable<object[]> AllColors()
        {
            yield return new object[] { OutputColor.Black, ConsoleColor.Black };
            yield return new object[] { OutputColor.DarkBlue, ConsoleColor.DarkBlue };
            yield return new object[] { OutputColor.DarkGreen, ConsoleColor.DarkGreen };
            yield return new object[] { OutputColor.DarkCyan, ConsoleColor.DarkCyan };
            yield return new object[] { OutputColor.DarkRed, ConsoleColor.DarkRed };
            yield return new object[] { OutputColor.DarkMagenta, ConsoleColor.DarkMagenta };
            yield return new object[] { OutputColor.DarkYellow, ConsoleColor.DarkYellow };
            yield return new object[] { OutputColor.Gray, ConsoleColor.Gray };
            yield return new object[] { OutputColor.DarkGray, ConsoleColor.DarkGray };
            yield return new object[] { OutputColor.Blue, ConsoleColor.Blue };
            yield return new object[] { OutputColor.Green, ConsoleColor.Green };
            yield return new object[] { OutputColor.Cyan, ConsoleColor.Cyan };
            yield return new object[] { OutputColor.Red, ConsoleColor.Red };
            yield return new object[] { OutputColor.Magenta, ConsoleColor.Magenta };
            yield return new object[] { OutputColor.Yellow, ConsoleColor.Yellow };
            yield return new object[] { OutputColor.White, ConsoleColor.White };
        }

        [Fact]
        public void InvalidOutputColorThrowsErrorWhenConverting()
        {
            Assert.Throws<ArgumentException>(() => ((OutputColor)99999).ToConsoleColor());
        }

        [Fact]
        public void InvalidConsoleColorThrowsErrorWhenConverting()
        {
            Assert.Throws<ArgumentException>(() => ((ConsoleColor)99999).ToOutputColor());
        }

        [Theory]
        [MemberData(nameof(AllColors))]
        public void OutputColorConvertsCorrectlyToConsoleColor(OutputColor outputColor, ConsoleColor consoleColor)
        {
            Assert.Equal(consoleColor, outputColor.ToConsoleColor());
        }

        [Theory]
        [MemberData(nameof(AllColors))]
        public void ConsoleColorConvertsCorrectlyToOutputColor(OutputColor outputColor, ConsoleColor consoleColor)
        {
            Assert.Equal(outputColor, consoleColor.ToOutputColor());
        }

    }
}

using EasyCli;
using UnitTests.mocks;
using Xunit;

namespace UnitTests
{
    public class RunCommandTests : TestHelper
    {
        #region Buildup
        internal RunCommandTests()
        {
            helloCommand = CommandMock.Create(new string[] { "hello", "hi" }, new string[] { "This is the hello command" });
            echoCommand = CommandMock.Create(new string[] { "echo", "helloShell" }, new string[] { "This is the echo command" });
        }

        private CommandMock helloCommand;
        private CommandMock echoCommand;
        #endregion

        #region Throws correct errors
        [Theory]
        [MemberData(nameof(AllCLIs))]
        public void ThrowsWhenNoCommandsRegistered(ICli cli)
        {
            Assert.Throws<CommandNotFoundException>(() => cli.RunCommand("hello"));
        }

        [Theory]
        [MemberData(nameof(AllCLIs))]
        public void ThrowsWhenOtherCommandsRegistered(ICli cli)
        {
            cli.RegisterCommand(helloCommand);

            Assert.Throws<CommandNotFoundException>(() => cli.RunCommand("helloShell"));
        }
        #endregion

        #region Command was run
        [Theory]
        [MemberData(nameof(AllCLIs))]
        public void CommandWasRun(ICli cli)
        {
            cli.RegisterCommand(helloCommand);

            cli.RunCommand("hello");
            cli.RunCommand("hi");

            Assert.Equal(2, helloCommand.CallCount);
        }

        [Theory]
        [MemberData(nameof(AllCLIs))]
        public void CommandWasRunMultipleTimes(ICli cli)
        {
            cli.RegisterCommand(helloCommand);

            Assert.Equal(0, helloCommand.CallCount);
            for (int i = 1; i <= 10; ++i)
            {
                cli.RunCommand("hello");
                Assert.Equal(i, helloCommand.CallCount);
            }
        }

        [Theory]
        [MemberData(nameof(AllCLIs))]
        public void CommandWasRunWithMultipleCommandsActive(ICli cli)
        {
            cli.RegisterCommands(new ICommand[] { helloCommand, echoCommand});

            cli.RunCommand("hello");

            Assert.Equal(1, helloCommand.CallCount);
            Assert.Equal(0, echoCommand.CallCount);
        }

        [Theory]
        [MemberData(nameof(AllCLIs))]
        public void CommandWasRunWithWhitespaceAround(ICli cli)
        {
            cli.RegisterCommand(helloCommand);

            cli.RunCommand("  hello   ");
            cli.RunCommand('\t' + "hi" + '\t');

            Assert.Equal(2, helloCommand.CallCount);
        }
        #endregion

        // TODO design command result and add tests here
    }
}

using EasyCli;

namespace UnitTests.mocks
{
    internal class CommandResultMock : ICommandResult
    {
        public static CommandResultMock Create()
        {
            return new CommandResultMock();
        }
    }
}
using EasyCli;

namespace UnitTests.mocks
{
    internal class CommandMock : ICommand
    {
        public string[] Names { get; private set; }
        public CommandMethod Method { get; private set; }
        public string[] Description { get; private set; }

        public int CallCount { get; private set; } = 0;

        public static CommandMock CreateDefault()
        {
            return Create(
                           names: new string[] { "mockCommand" },
                           description: new string[] { "A mock Command" }
                       );
        }

        public static CommandMock Create(string[] names, string[] description)
        {
            return new CommandMock(names, description);
        }

        public static CommandMock CreateDefaultWrapper(CommandMethod innerCmd)
        {
            return CreateWrapper(
                               names: new string[] { "mockCommand" },
                               innerCmd: innerCmd,
                               description: new string[] { "A mock Command" }
                           );
        }

        public static CommandMock CreateWrapper(string[] names, CommandMethod innerCmd, string[] description)
        {
            return new CommandMock(names, innerCmd, description);
        }

        public static ICommandResult MockCommandMethod(ICli cli, IArgumentsInfo argInfo)
        {
            return CommandResultMock.Create();
        }

        private CommandMock(string[] names, string[] description)
        {
            Names = names;
            Description = description;
            Method = CreateCommandMethod();
        }

        private CommandMock(string[] names, CommandMethod innerCmd, string[] description)
        {
            Names = names;
            Description = description;
            Method = CreateCommandMethodWrapper(innerCmd);
        }

        private CommandMethod CreateCommandMethod()
        {
            return (ICli cli, IArgumentsInfo argInfo) =>
                         {
                             ++CallCount;
                             return CommandResultMock.Create();
                         };
        }

        private CommandMethod CreateCommandMethodWrapper(CommandMethod cmdM)
        {
            return (ICli cli, IArgumentsInfo argInfo) =>
                         {
                             ++CallCount;
                             return cmdM(cli, argInfo);
                         };
        }
    }
}

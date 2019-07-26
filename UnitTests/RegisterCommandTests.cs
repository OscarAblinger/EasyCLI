using EasyCli;
using System.Collections.Generic;
using System.Linq;
using UnitTests.mocks;
using Xunit;

namespace UnitTests
{
    public class RegisterCommandTests : TestHelper
    {
        #region Buildup
        public RegisterCommandTests() : base()
        {
            command = CommandMock.CreateDefault();
            command2 = CommandMock.Create(new string[] { "mock2" }, new string[] { "mockDescripton" });
        }

        private ICommand command;
        private ICommand command2;
        #endregion

        #region Theory sets
        public readonly static string[] ValidNameOptions = new string[] { "mockString" };
        public readonly static string[][] ValidNamesArrayOptions = new string[][]
        {
            new string[] { "mockName" },
            new string[] { "mockName1", "mockName2" },
            new string[] { "mockName1", "mockName2", "mockName3" }
        };
        public readonly static string[] ValidDescriptionOptions = new string[] { null, "", "mockDescription" };
        public readonly static string[][] ValidDescriptionArrayOptions = new string[][] {
            null,
            new string[0],
            new string[] { null } ,
            new string[] { "" },
            new string[] { "mockDescription" },
            new string[] { null, "", "mockDescription" }
        };

        private static object[][] ToObjMatrix<T>(IEnumerable<T> elem)
        {
            return elem.Select(e => new object[] { e }).ToArray();
        }

        public static IEnumerable<object[]> CLINameCombinations()
        {
            return AllMergeCombinations(AllCLIs(), ToObjMatrix(ValidNameOptions));
        }

        public static IEnumerable<object[]> CLIDescriptionCombinations()
        {
            return AllMergeCombinations(AllCLIs(), ToObjMatrix(ValidDescriptionOptions));
        }

        public static IEnumerable<object[]> CLINameDescriptionCombinations()
        {
            return AllMergeCombinations(AllCLIs(),
                AllMergeCombinations(
                    ToObjMatrix(ValidNameOptions),
                    ToObjMatrix(ValidDescriptionOptions)
                    ));
        }

        public static IEnumerable<object[]> CLINamesArrayDescriptionCombinations()
        {
            return AllMergeCombinations(AllCLIs(),
                AllMergeCombinations(
                    ToObjMatrix(ValidNamesArrayOptions),
                    ToObjMatrix(ValidDescriptionOptions)
                    ));
        }

        public static IEnumerable<object[]> CLINameDescriptionArrayCombinations()
        {
            return AllMergeCombinations(AllCLIs(),
                AllMergeCombinations(
                    ToObjMatrix(ValidNameOptions),
                    ToObjMatrix(ValidDescriptionArrayOptions)
                    ));
        }

        public static IEnumerable<object[]> CLINamesArrayDescriptionArrayCombinations()
        {
            return AllMergeCombinations(AllCLIs(),
                AllMergeCombinations(
                    ToObjMatrix(ValidNamesArrayOptions),
                    ToObjMatrix(ValidDescriptionArrayOptions)
                    ));
        }
        #endregion

        #region Return Same CLI
        [Theory]
        [MemberData(nameof(AllCLIs))]
        public void ReturnSameCLI1(ICli cli)
        {
            Assert.Same(cli, cli.RegisterCommand(command));
        }

        [Theory]
        [MemberData(nameof(AllCLIs))]
        public void ReturnSameCLI2(ICli cli)
        {
            Assert.Same(cli, cli.RegisterCommands(new ICommand[] { command, command2 }));
        }

        [Theory]
        [MemberData(nameof(CLIDescriptionCombinations))]
        public void ReturnSameCLI3(ICli cli, string description)
        {
            Assert.Equal(cli, cli.RegisterCommand("mock", CommandMock.MockCommandMethod, description));
        }

        [Theory]
        [MemberData(nameof(CLINameDescriptionCombinations))]
        public void ReturnSameCLI4(ICli cli, string name, string description)
        {
            Assert.Equal(cli, cli.RegisterCommand(name, CommandMock.MockCommandMethod, description));
        }

        [Theory]
        [MemberData(nameof(CLINamesArrayDescriptionCombinations))]
        public void ReturnSameCLI5(ICli cli, string[] names, string description)
        {
            Assert.Equal(cli, cli.RegisterCommand(names, CommandMock.MockCommandMethod, description));
        }

        [Theory]
        [MemberData(nameof(CLINameDescriptionArrayCombinations))]
        public void ReturnSameCLI6(ICli cli, string names, string[] description)
        {
            Assert.Equal(cli, cli.RegisterCommand(names, CommandMock.MockCommandMethod, description));
        }

        [Theory]
        [MemberData(nameof(CLINamesArrayDescriptionArrayCombinations))]
        public void ReturnSameCLI7(ICli cli, string[] names, string[] description)
        {
            Assert.Equal(cli, cli.RegisterCommand(names, CommandMock.MockCommandMethod, description));
        }
        #endregion

        #region Command was Added
        [Theory]
        [MemberData(nameof(AllCLIs))]
        public void CommandWasAdded1(ICli cli)
        {
            cli.RegisterCommand(command);
            Assert.Single(cli.GetCommands(), command);
        }

        [Theory]
        [MemberData(nameof(AllCLIs))]
        public void CommandWasAdded2(ICli cli)
        {
            cli.RegisterCommands(new ICommand[] { command, command2 });
            Assert.Equal(cli.GetCommands().ToHashSet(), new HashSet<ICommand>() { command, command2 });
        }

        [Theory]
        [MemberData(nameof(AllCLIs))]
        public void CommandWasAdded3(ICli cli)
        {
            cli.RegisterCommand("mock", CommandMock.MockCommandMethod);
            Assert.Single(cli.GetCommands());

            var el = cli.GetCommands()[0];
            Assert.Equal(new string[] { "mock" }, el.Names);
            Assert.Same((CommandMethod)CommandMock.MockCommandMethod, el.Method);
            Assert.Equal(new string[0], el.Description);
        }

        [Theory]
        [MemberData(nameof(CLINameDescriptionCombinations))]
        public void CommandWasAdded4(ICli cli, string name, string description)
        {
            cli.RegisterCommand(name, CommandMock.MockCommandMethod, description);
            Assert.Single(cli.GetCommands());

            var el = cli.GetCommands()[0];
            Assert.Equal(new string[] { name }, el.Names);
            Assert.Same((CommandMethod)CommandMock.MockCommandMethod, el.Method);
            Assert.Equal(new string[] { description }, el.Description);
        }

        [Theory]
        [MemberData(nameof(CLINamesArrayDescriptionCombinations))]
        public void CommandWasAdded5(ICli cli, string[] names, string description)
        {
            cli.RegisterCommand(names, CommandMock.MockCommandMethod, description);
            Assert.Single(cli.GetCommands());

            var el = cli.GetCommands()[0];
            Assert.Equal(names, el.Names);
            Assert.Same((CommandMethod)CommandMock.MockCommandMethod, el.Method);
            Assert.Equal(new string[] { description }, el.Description);
        }

        [Theory]
        [MemberData(nameof(CLINameDescriptionArrayCombinations))]
        public void CommandWasAdded6(ICli cli, string name, string[] description)
        {
            cli.RegisterCommand(name, CommandMock.MockCommandMethod, description);
            Assert.Single(cli.GetCommands());

            var el = cli.GetCommands()[0];
            Assert.Equal(new string[] { name }, el.Names);
            Assert.Same((CommandMethod)CommandMock.MockCommandMethod, el.Method);
            Assert.Equal(description, el.Description);
        }

        [Theory]
        [MemberData(nameof(CLINamesArrayDescriptionArrayCombinations))]
        public void CommandWasAdded7(ICli cli, string[] names, string[] description)
        {
            cli.RegisterCommand(names, CommandMock.MockCommandMethod, description);
            Assert.Single(cli.GetCommands());

            var el = cli.GetCommands()[0];
            Assert.Equal(names, el.Names);
            Assert.Same((CommandMethod)CommandMock.MockCommandMethod, el.Method);
            Assert.Equal(description, el.Description);
        }
        #endregion
    }
}

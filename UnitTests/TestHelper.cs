using EasyCli;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTests.Helpers;
using UnitTests.mocks;

namespace UnitTests
{
    abstract public class TestHelper
    {
        public static IEnumerable<Func<ICli>> AllCLICreators()
        {
            yield return CliFactory.Create;
            yield return () => CliFactory.CreateWithDefaults();

            /*
            var parameterizedClis = GetParameterizedClis();

            foreach (var cli in parameterizedClis)
            {
                yield return cli;
            }
            */
        }

        private static IEnumerable<Func<ICli>> GetParameterizedClis()
        {
            return CombinationHelper.GetWithParameterCombinations(
                new OneOf<string, string[], Action<ICli>>[] {
                    null,
                    "hi o/",
                    new string[] { "hello o/", "" },
                    (Action<ICli>)((cli) => cli.Out.WriteLine("Hello there o/"))
                },
                new Either<string, Action<ICli>>[]
                {
                    null,
                    "Please issue a command : ",
                    (Action<ICli>)((cli) => cli.Out.WriteLine("Please input the next command:"))
                },
                new ExceptionCatchHandler[] {
                    null,
                    (cli, ex) => cli.Out.WriteLine(ex),
                    (cli, ex) => cli.Out.WriteLine("Found exception: " + ex.Message),
                },
                new IList<ICommand>[] {
                    null,
                    new List<ICommand>(),
                    new List<ICommand>()
                    {
                        CommandMock.CreateDefault()
                    }
                },
                (greeting, prompt, exceptionCatchHandler, commands) =>
                {
                    return (Func<ICli>)(() =>
                        CliFactory.CreateWithDefaults(greeting, prompt, exceptionCatchHandler, commands));
                }
                );
        }

        public static IEnumerable<object[]> AllCLIs()
        {
            return AllCLICreators().Select(creator => new object[] { creator() });
        }

        public static IEnumerable<object[]> CliMergeWithParameters(IEnumerable<object[]> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentException("The other parameters may not be null");
            }

            return from p in parameters
                   from creator in AllCLICreators()
                   select p.Prepend(creator()).ToArray();
        }

        public static IEnumerable<object[]> AllMergeCombinations(IEnumerable<object[]> first, IEnumerable<object[]> second)
        {
            if (first == null || second == null)
            {
                throw new ArgumentException("Neither of the two IEnumerables may be null");
            }

            return from f in first
                   from s in second
                   select f.Concat(s).ToArray();
        }
    }
}
using EasyCli;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    abstract public class TestHelper
    {
        public static IEnumerable<Func<ICli>> AllCLICreators()
        {
            yield return CliFactory.Create;
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
using EasyCli;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    abstract public class TestHelper
    {
        public static IEnumerable<object[]> AllCLIs()
        {
            yield return new object[] { CliFactory.Create() };
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
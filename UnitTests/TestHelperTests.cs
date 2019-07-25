using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class TestHelperTests
    {
        [Fact]
        public void AllCombinationsThrowsErrorsCorrectlyWithStrings()
        {
            // ToArray is necessary since Combinations are lazily called
            Assert.Throws<ArgumentException>(() =>
                TestHelper.AllCombinations(new string[] { "" }, 0).ToArray());
            Assert.Throws<ArgumentException>(() =>
                TestHelper.AllCombinations(new string[] { "" }, -1).ToArray());

            Assert.Throws<ArgumentException>(() =>
                TestHelper.AllCombinations<string>(null).ToArray());
            Assert.Throws<ArgumentException>(() =>
                TestHelper.AllCombinations(new string[] { }).ToArray());
        }

        private static object[] allStringTypes = new string[]
        {
            null,
            "",
            "test"
        };

        private static ISet<object[]> allStringTypes1Combination = new HashSet<object[]>
        {
            new object[] { null },
            new object[] { "" },
            new object[] { "test" },
        };

        private static ISet<object[]> allStringTypes2Combination = new HashSet<object[]>
        {
            new object[] { null, null },
            new object[] { "", null },
            new object[] { "test", null },
            new object[] { null, "" },
            new object[] { "", "" },
            new object[] { "test", "" },
            new object[] { null, "test" },
            new object[] { "", "test" },
            new object[] { "test", "test" },
        };

        private static ISet<object[]> allStringTypes3Combination = new HashSet<object[]>
        {
            new object[] { null, null, null },
            new object[] { "", null, null },
            new object[] { "test", null, null },
            new object[] { null, "", null },
            new object[] { "", "", null },
            new object[] { "test", "", null },
            new object[] { null, "test", null },
            new object[] { "", "test", null },
            new object[] { "test", "test", null },

            new object[] { null, null, "" },
            new object[] { "", null, "" },
            new object[] { "test", null, "" },
            new object[] { null, "", "" },
            new object[] { "", "", "" },
            new object[] { "test", "", "" },
            new object[] { null, "test", "" },
            new object[] { "", "test", "" },
            new object[] { "test", "test", "" },

            new object[] { null, null, "test" },
            new object[] { "", null, "test" },
            new object[] { "test", null, "test" },
            new object[] { null, "", "test" },
            new object[] { "", "", "test" },
            new object[] { "test", "", "test" },
            new object[] { null, "test", "test" },
            new object[] { "", "test", "test" },
            new object[] { "test", "test", "test" },
        };

        [Fact]
        public void AllCombinationsIsCorrectWithOneEntry()
        {
            var result = TestHelper.AllCombinations(allStringTypes);

            Assert.Equal(allStringTypes1Combination, result.ToHashSet());
        }

        [Fact]
        public void AllCombinationsIsCorrectWithTwoEntries()
        {
            var result = TestHelper.AllCombinations(allStringTypes, 2);

            Assert.Equal(allStringTypes2Combination, result.ToHashSet());
        }

        [Fact]
        public void AllCombinationsIsCorrectWithThreeEntries()
        {
            var result = TestHelper.AllCombinations(allStringTypes, 3);

            Assert.Equal(allStringTypes3Combination, result.ToHashSet());
        }
    }
}

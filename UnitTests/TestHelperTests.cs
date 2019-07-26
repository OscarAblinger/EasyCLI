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

        private readonly static ISet<object[]> allStringTypes1Combination = new HashSet<object[]>
        {
            new object[] { null },
            new object[] { "" },
            new object[] { "test" },
        };

        private readonly static ISet<object[]> allStringTypes2Combination = new HashSet<object[]>
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

        private readonly static ISet<object[]> allStringTypes3Combination = new HashSet<object[]>
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

        [Fact]
        public void AllMergeCombinationsThrowsErrorsCorrectlyWithStrings()
        {
            var notNullEnum = new object[][] { new object[0] }.AsEnumerable();

            Assert.Throws<ArgumentException>(() => TestHelper.AllMergeCombinations(null, null));
            Assert.Throws<ArgumentException>(() => TestHelper.AllMergeCombinations(null, notNullEnum));
            Assert.Throws<ArgumentException>(() => TestHelper.AllMergeCombinations(notNullEnum, null));
        }

        [Fact]
        public void AllMergeCombinationsMergesCorrectlyWithEmptyEnumerations()
        {
            var emptyEnum = new object[0][].AsEnumerable();
            var mergeCombination = TestHelper.AllMergeCombinations(emptyEnum, emptyEnum);

            Assert.NotNull(mergeCombination);
            Assert.Empty(mergeCombination);
        }

        [Fact]
        public void AllMergeCombinationsMergesCorrectlyWithOneEmptyEnumeration()
        {
            var emptyEnum = new object[0][].AsEnumerable();
            var fullEnum = new object[][] { new object[] { "hello", "world" } }.AsEnumerable();
            var mergeCombination1 = TestHelper.AllMergeCombinations(emptyEnum, fullEnum);
            var mergeCombination2 = TestHelper.AllMergeCombinations(emptyEnum, fullEnum);

            Assert.NotNull(mergeCombination1);
            Assert.Empty(mergeCombination1);

            Assert.NotNull(mergeCombination2);
            Assert.Empty(mergeCombination2);
        }

        private static readonly object[][] singleStringArray = new object[][] { new object[] { "ssa" } };
        private static readonly object[][] multipleStringArray = new object[][]
        {
            new object[] { "hello", "world" },
            new object[] { "foo", "bar" }
        };

        [Fact]
        public void AllMergeCombinationsMergesCorrectlyWithTwoEnumerations()
        {
            AssertCorrectMerge(
                new object[][] { new object[] { "prefix" } },
                new object[][] { new object[] { "" } },
                (res) =>
                {
                    Assert.Equal("prefix", res[0]);
                    Assert.Equal("", res[1]);
                });
            AssertCorrectMerge(
                singleStringArray,
                multipleStringArray,
                (res) =>
                {
                    Assert.Equal(singleStringArray[0][0], res[0]);
                    Assert.Contains(res[1], multipleStringArray.Select(arr => arr[0]));
                    Assert.Contains(res[2], multipleStringArray.Select(arr => arr[1]));
                }
                );
            AssertCorrectMerge(
                multipleStringArray,
                singleStringArray,
                (res) =>
                {
                    Assert.Contains(res[0], multipleStringArray.Select(arr => arr[0]));
                    Assert.Contains(res[1], multipleStringArray.Select(arr => arr[1]));
                    Assert.Equal(singleStringArray[0][0], res[2]);
                }
                );
        }

        private void AssertCorrectMerge(object[][] first, object[][] second, Action<object[]> resultValidator)
        {
            var combinationResult = TestHelper.AllMergeCombinations(first, second).ToHashSet();

            Assert.Equal(first.Length * second.Length, combinationResult.Count());
            Assert.All(combinationResult, resultValidator);
        }
    }
}

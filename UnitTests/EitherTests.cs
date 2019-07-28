using EasyCli;
using Xunit;

namespace UnitTests
{
    public class EitherTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("String test")]
        [InlineData(null)]
        public void FirstIsSelected(string testStr)
        {
            Either<string, int> strEither = new Either<string, int>(testStr);

            Assert.True(strEither.IsFirst);
            Assert.Equal(testStr, strEither.First);
        }

        [Theory]
        [InlineData("")]
        [InlineData("String test")]
        [InlineData(null)]
        public void MatchActionSelectsFirstCorrectly(string testStr)
        {
            Either<string, int> strEither = new Either<string, int>(testStr);

            strEither.Match(
                (str) => Assert.Equal(testStr, str),
                (i) => Assert.True(false, "second is matched, but first was expected")
                );
        }

        [Theory]
        [InlineData("")]
        [InlineData("String test")]
        [InlineData(null)]
        public void MatchFunctionSelectsFirstCorrectly(string testStr)
        {
            Either<string, int> strEither = new Either<string, int>(testStr);

            strEither.Match(
                (str) =>
                {
                    Assert.Equal(testStr, str);
                    return true;
                },
                (i) =>
                {
                    Assert.True(false, "second is matched, but first was expected");
                    return false;
                }
                );
        }

        [Fact]
        public void SecondIsSelected()
        {
            int testInt = 5;

            Either<string, int> intEither = new Either<string, int>(testInt);

            Assert.False(intEither.IsFirst);
            Assert.Equal(testInt, intEither.Second);
        }

        [Fact]
        public void MatchActionSelectsSecondCorrectly()
        {
            int testInt = 5;

            Either<string, int> intEither = new Either<string, int>(testInt);

            intEither.Match(
                (str) => Assert.True(false, "first is matched, but second was expected"),
                (i) => Assert.Equal(testInt, i)
                );
        }

        [Fact]
        public void MatchFunctionSelectsSecondCorrectly()
        {
            int testInt = 5;

            Either<string, int> intEither = new Either<string, int>(testInt);

            intEither.Match(
                (str) =>
                {
                    Assert.True(false, "first is matched, but second was expected");
                    return false;
                },
                (i) =>
                {
                    Assert.Equal(testInt, i);
                    return true;
                }
                );
        }
    }
}

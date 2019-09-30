using EasyCli;
using Xunit;

namespace UnitTests
{
    public class OneOfTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("String test")]
        [InlineData(null)]
        public void FirstIsSelected(string testStr)
        {
            OneOf<string, int, float> strEither = new OneOf<string, int, float>(testStr);

            Assert.Equal(OneOf.WhichType.first, strEither.FilledType);
            Assert.Equal(testStr, strEither.First);
        }

        [Theory]
        [InlineData("")]
        [InlineData("String test")]
        [InlineData(null)]
        public void MatchActionSelectsFirstCorrectly(string testStr)
        {
            OneOf<string, int, float> strEither = new OneOf<string, int, float>(testStr);

            strEither.Match(
                (str) => Assert.Equal(testStr, str),
                (i) => Assert.True(false, "second is matched, but first was expected"),
                (f) => Assert.True(false, "third is matched, but first was expected")
                );
        }

        [Theory]
        [InlineData("")]
        [InlineData("String test")]
        [InlineData(null)]
        public void MatchFunctionSelectsFirstCorrectly(string testStr)
        {
            OneOf<string, int, float> strEither = new OneOf<string, int, float>(testStr);

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
                },
                (f) =>
                {
                    Assert.True(false, "third is matched, but first was expected");
                    return false;

                }
                );
        }

        [Fact]
        public void SecondIsSelected()
        {
            int testInt = 5;

            OneOf<string, int, float> intEither = new OneOf<string, int, float>(testInt);

            Assert.Equal(OneOf.WhichType.second, intEither.FilledType);
            Assert.Equal(testInt, intEither.Second);
        }

        [Fact]
        public void MatchActionSelectsSecondCorrectly()
        {
            int testInt = 5;

            OneOf<string, int, float> intEither = new OneOf<string, int, float>(testInt);

            intEither.Match(
                (str) => Assert.True(false, "first is matched, but second was expected"),
                (i) => Assert.Equal(testInt, i),
                (f) => Assert.True(false, "third is matched, but second was expected")
                );
        }

        [Fact]
        public void MatchFunctionSelectsSecondCorrectly()
        {
            int testInt = 5;

            OneOf<string, int, float> intEither = new OneOf<string, int, float>(testInt);

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
                },
                (f) =>
                {
                    Assert.True(false, "third is matched, but second was expected");
                    return false;
                }
                );
        }

        [Fact]
        public void ThirdIsSelected()
        {
            float testFloat = 5;

            OneOf<string, int, float> intEither = new OneOf<string, int, float>(testFloat);

            Assert.Equal(OneOf.WhichType.third, intEither.FilledType);
            Assert.Equal(testFloat, intEither.Third);
        }

        [Fact]
        public void MatchActionSelectsThirdCorrectly()
        {
            float testFloat = 5;

            OneOf<string, int, float> intEither = new OneOf<string, int, float>(testFloat);

            intEither.Match(
                (str) => Assert.True(false, "first is matched, but third was expected"),
                (i) => Assert.True(false, "second is matched, but third was expected"),
                (f) => Assert.Equal(testFloat, f)
                );
        }

        [Fact]
        public void MatchFunctionSelectsThirdCorrectly()
        {
            float testFloat = 5;

            OneOf<string, int, float> intEither = new OneOf<string, int, float>(testFloat);

            intEither.Match(
                (str) =>
                {
                    Assert.True(false, "first is matched, but third was expected");
                    return false;
                },
                (i) =>
                {
                    Assert.True(false, "second is matched, but third was expected");
                    return true;
                },
                (f) =>
                {
                    Assert.Equal(testFloat, f);
                    return false;
                }
                );
        }

        [Fact]
        public void ImplicitConversionForFirstWorks()
        {
            OneOf<string, int, float> implicitStr = "implicitString";
            Assert.Equal(new OneOf<string, int, float>("implicitString"), implicitStr);
        }

        [Fact]
        public void ImplicitConversionForSecondWorks()
        {
            OneOf<string, int, float> implicitInt = 5;
            Assert.Equal(new OneOf<string, int, float>(5), implicitInt);
        }

        [Fact]
        public void ImplicitConversionForThirdWorks()
        {
            float f = 5.3f;
            OneOf<string, int, float> implicitFloat = f;
            Assert.Equal(new OneOf<string, int, float>(f), implicitFloat);
        }
    }
}

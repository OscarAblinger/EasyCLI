using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.Helpers
{
    public static class CombinationHelper
    {
        public static IEnumerable<R> GetWithParameterCombinations<R, T>(IEnumerable<T> params1, Func<T, R> func)
        {
            foreach (var p1 in params1)
            {
                yield return func(p1);
            }
        }

        public static IEnumerable<R> GetWithParameterCombinations<R, T1, T2>(IEnumerable<T1> params1, IEnumerable<T2> params2, Func<T1, T2, R> func)
        {
            return GetWithParameterCombinations(params1, (p1) =>
                GetWithParameterCombinations(params2, (p2) => func(p1, p2)))
                .SelectMany(en => en);
        }

        public static IEnumerable<R> GetWithParameterCombinations<R, T1, T2, T3>(IEnumerable<T1> params1, IEnumerable<T2> params2, IEnumerable<T3> params3, Func<T1, T2, T3, R> func)
        {
            return GetWithParameterCombinations(params1, params2, (p1, p2) =>
                GetWithParameterCombinations(params3, (p3) => func(p1, p2, p3)))
                .SelectMany(en => en);
        }

        public static IEnumerable<R> GetWithParameterCombinations<R, T1, T2, T3, T4>(
            IEnumerable<T1> params1,
            IEnumerable<T2> params2,
            IEnumerable<T3> params3,
            IEnumerable<T4> params4,
            Func<T1, T2, T3, T4, R> func)
        {
            return GetWithParameterCombinations(params1, params2, params3, (p1, p2, p3) =>
                GetWithParameterCombinations(params4, (p4) => func(p1, p2, p3, p4)))
                .SelectMany(en => en);
        }
    }
}

using System;
using System.Collections.Generic;

namespace UnitTests
{
    abstract public class TestHelper
    {
        public static IEnumerable<object[]> AllCombinations<T>(T[] uniqueList, int nrOfResults = 1)
        {
            if (nrOfResults < 1)
                throw new ArgumentException("Nr of Results has to be at least 1");
            if (uniqueList == null || uniqueList.Length == 0)
                throw new ArgumentException("The unique list may not be null and must have at least one element");

            int[] indices = new int[nrOfResults];

            while(true)
            {
                yield return CreateObjectWithIndices(uniqueList, indices);

                bool iteratedThroughAllIndices = IncreaseIndices(indices, uniqueList.Length);

                if (iteratedThroughAllIndices)
                    break;
            }
        }

        private static object[] CreateObjectWithIndices<T>(T[] uniqueList, int[] indices)
        {
            var resultObject = new object[indices.Length];

            for(int i = 0; i < indices.Length; ++i)
            {
                resultObject[i] = uniqueList[indices[i]];
            }

            return resultObject;
        }

        private static bool IncreaseIndices(int[] indices, int nrOfUniqueData)
        {
            int i = 0;
            while(i < indices.Length)
            {
                ++indices[i];

                if (indices[i] >= nrOfUniqueData)
                    indices[i] = 0;
                else
                    return false;

                ++i;
            }
            return true;
        }
    }
}

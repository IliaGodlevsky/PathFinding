using Common.Extensions;
using System.Linq;

namespace Common
{
    public static class EnumerableHelper
    {
        public static bool[] GetBools(int totalSize, int trueValues)
        {
            if (trueValues > totalSize)
            {
                trueValues = totalSize;
            }

            int numberOfTrueValues = totalSize.GetPercentage(trueValues);
            var regulars = Enumerable.Repeat(false, totalSize - numberOfTrueValues);
            var obstacles = Enumerable.Repeat(true, numberOfTrueValues);
            return regulars.Concat(obstacles).ToArray();
        }
    }
}

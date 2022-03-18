using System.Linq;

namespace Common.Extensions
{
    public static class ValueTupleExtensions
    {
        public static bool[] ToBoolsArray(this (int totalSize, int trueValues) parametres)
        {
            if (parametres.trueValues > parametres.totalSize)
            {
                parametres.trueValues = parametres.totalSize;
            }

            int numberOfTrueValues = parametres.totalSize.GetPercentage(parametres.trueValues);
            var regulars = Enumerable.Repeat(false, parametres.totalSize - numberOfTrueValues);
            var obstacles = Enumerable.Repeat(true, numberOfTrueValues);
            return regulars.Concat(obstacles).ToArray();
        }

        public static T[] Merge<T>(this (T first, T second) elements)
        {
            return new T[] { elements.first, elements.second };
        }
    }
}

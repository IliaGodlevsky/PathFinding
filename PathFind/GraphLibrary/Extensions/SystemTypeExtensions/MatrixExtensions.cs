using System;
using System.Drawing;
using System.Linq;

namespace GraphLibrary.Extensions.SystemTypeExtensions
{
    public static class MatrixExtensions
    {
        public static int Width<TSource>(this TSource[,] arr)
        {
            if (arr == null)
                return 0;
            return arr.GetLength(dimension: 0);
        }

        public static int Height<TSource>(this TSource[,] arr)
        {
            if (arr == null)
                return 0;
            else if (arr.Width() == 0)
                return 0;
            return arr.Length / arr.Width();
        }

        public static TKey[,] Accumulate<TSource, TKey>(this TSource[,] arr,
            Func<TSource, TKey> func)
        {
            var outArray = new TKey[arr.Width(), arr.Height()];
            for (int x = 0; x < arr.Width(); x++)
                for (int y = 0; y < arr.Height(); y++)
                    outArray[x, y] = func(arr[x, y]);
            return outArray;
        }

        public static void Apply<TSource>(this TSource[,] arr,
            params Func<TSource, TSource>[] methods)
        {
            for (int x = 0; x < arr.Width(); x++)
                for (int y = 0; y < arr.Height(); y++)
                    foreach (var method in methods)
                        arr[x, y] = method(arr[x, y]);
        }

        public static Point GetIndices<TSource>(this TSource[,] arr, TSource item)
        {
            var index = Array.IndexOf(arr.Cast<TSource>().ToArray(), item);
            var yCoordinate = (arr.Height() + index) % arr.Height();
            var xCoordinate = (int)Math.Ceiling((decimal)(index - yCoordinate) / arr.Height());
            return new Point(xCoordinate, yCoordinate);
        }
    }
}

using GraphLibrary.Vertex.Interface;
using System;
using System.Linq;
using System.Threading;

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

        private static void Apply<TSource>(TSource [,] arr, 
            int endFrom, int startFrom,
            params Func<TSource, TSource>[] methods)
        {
            for (int x = startFrom; x < endFrom; x++)
                for (int y = 0; y < arr.Height(); y++)
                    foreach (var method in methods)
                        arr[x, y] = method(arr[x, y]);
        }

        public static void Apply<TSource>(this TSource[,] arr,
            params Func<TSource, TSource>[] methods)
        {
            Apply(arr, arr.Width(), 0, methods);
        }

        public static void ApplyParallel<TSource>(this TSource[,] arr,
            params Func<TSource, TSource>[] methods)
        {
            var threads = Environment.ProcessorCount;
            var threadPool = new Thread[threads];
            for (int i = 0; i < threads; i++)
            {
                var start = arr.Width() * i / threads;
                var end = arr.Width() * (i + 1) / threads;
                threadPool[i] = new Thread(() => Apply(arr, end, start, methods));
            }
            foreach (var thread in threadPool) thread.Start();
            foreach (var thread in threadPool) thread.Join();
        }

        public static Position GetIndices<TSource>(this TSource[,] arr, TSource item)
        {
            // an ancient math magic of Jedi. It works, believe me))
            var index = Array.IndexOf(arr.Cast<TSource>().ToArray(), item);
            var yCoordinate = (arr.Height() + index) % arr.Height();
            var xCoordinate = (int)Math.Ceiling((decimal)(index - yCoordinate) / arr.Height());
            return new Position(xCoordinate, yCoordinate);
        }
    }
}

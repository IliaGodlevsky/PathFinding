using GraphLibrary.Coordinates;
using GraphLibrary.Vertex.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GraphLibrary.Extensions.SystemTypeExtensions
{
    public static class MatrixExtensions
    {        
        public static int Width<TSource>(this TSource[,] arr)
        {
            return arr == null ? 0 : arr.GetLength(dimension: 0);
        }

        public static int Height<TSource>(this TSource[,] arr)
        {
            if (arr?.Width() == 0)
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
            int start, int end, params Func<TSource, TSource>[] methods)
        {
            for (int x = start; x < end; x++)
                for (int y = 0; y < arr.Height(); y++)
                    foreach (var method in methods)
                        arr[x, y] = method(arr[x, y]);            
        }

        public static void Apply<TSource>(this TSource[,] arr, params Func<TSource, TSource>[] methods) 
            => Apply(arr, 0, arr.Width(), methods);

        public static async void ApplyParallel<TSource>(this TSource[,] arr,
            params Func<TSource, TSource>[] methods)
        {
            if (arr.IsBigEnoughToParallel())
            {
                var tasks = Environment.ProcessorCount;
                var tasksPool = new Task[tasks];
                for (int i = 0; i < tasks; i++)
                {
                    var start = arr.Width() * i / tasks;
                    var end = arr.Width() * (i + 1) / tasks;
                    tasksPool[i] = new Task(() => Apply(arr, start, end, methods));
                }
                foreach (var task in tasksPool) task.Start();
                foreach (var task in tasksPool) await task;
            }
            else
                arr.Apply(methods);
        }

        public static Position GetIndices<TSource>(this TSource[,] arr, TSource item)
        {
            // an ancient math magic of Jedi. It works, believe me))
            var index = Array.IndexOf(arr.Cast<TSource>().ToArray(), item);
            var yCoordinate = (arr.Height() + index) % arr.Height();
            var xCoordinate = (int)Math.Ceiling((decimal)(index - yCoordinate) / arr.Height());
            return new Position(xCoordinate, yCoordinate);
        }

        private static bool IsBigEnoughToParallel<TSource>(this TSource[,] arr)
        {
            return arr.Length >= 200;
        }
    }
}

﻿using GraphLibrary.Coordinates;
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
            for (var x = 0; x < arr.Width(); x++)
                for (var y = 0; y < arr.Height(); y++)
                    outArray[x, y] = func(arr[x, y]);
            return outArray;
        }

        private static void Apply<TSource>(TSource [,] arr, 
            int start, int end, params Func<TSource, TSource>[] methods)
        {
            for (var x = start; x < end; x++)
                for (var y = 0; y < arr.Height(); y++)
                    foreach (var method in methods)
                        arr[x, y] = method(arr[x, y]);
        }

        /// <summary>
        /// Apply each method of methods to each element in array
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="arr"></param>
        /// <param name="methods"></param>
        public static void Apply<TSource>(this TSource[,] arr, params Func<TSource, TSource>[] methods) 
            => Apply(arr, 0, arr.Width(), methods);

        /// <summary>
        /// Uses all processors to parallelize function execution
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="arr"></param>
        /// <param name="methods">array of methods to apply to each element in array</param>
        public static async void ApplyParallel<TSource>(this TSource[,] arr,
            params Func<TSource, TSource>[] methods)
        {
            var tasks = Environment.ProcessorCount;
            var tasksPool = new Task[tasks];
            for (var i = 0; i < tasks; i++) 
            {
                var start = arr.Width() * i / tasks;
                var end = arr.Width() * (i + 1) / tasks;
                tasksPool[i] = new Task(() => Apply(arr, start, end, methods));
            }
            foreach (var task in tasksPool) task.Start();
            foreach (var task in tasksPool) await task;
        }

        public static Position GetIndices<TSource>(this TSource[,] arr, TSource item)
        {
            var index = Array.IndexOf(arr.Cast<TSource>().ToArray(), item);
            var yCoordinate = (arr.Height() + index) % arr.Height();
            var xCoordinate = (int)Math.Ceiling((decimal)(index - yCoordinate) / arr.Height());
            return new Position(xCoordinate, yCoordinate);
        }
    }
}

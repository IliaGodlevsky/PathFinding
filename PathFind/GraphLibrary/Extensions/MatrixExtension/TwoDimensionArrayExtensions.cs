using System;

namespace GraphLibrary.Extensions.MatrixExtension
{
    public static class TwoDimensionArrayExtensions
    {
        public static int Width<TSource>(this TSource[,] arr) => arr.GetLength(0);

        public static int Height<TSource>(this TSource[,] arr) => arr.Length / arr.Width();

        public static int CountIf<TSource>(this TSource[,] arr, Predicate<TSource> predicate)
        {
            int number = 0;
            foreach (var element in arr)
                if (predicate(element))
                    number++;
            return number;
        }

        public static TKey[,] Accumulate<TSource, TKey>(this TSource[,] arr, Func<TSource,TKey> func)
        {
            int width = arr.GetLength(0);
            int height = arr.Length / arr.GetLength(0);
            TKey[,] outArray = new TKey[width, height];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    outArray[i, j] = func(arr[i, j]);
            return outArray;
        }

        public static void Shuffle<TSource>(this TSource[,] arr)
        {
            Random rand = new Random();
            int a, b, c, d;
            TSource temp;
            for (int i = 0; i < arr.Width(); i++)
                for (int j = 0; j < arr.Height(); j++) 
                {
                    a = rand.Next(arr.Width());
                    b = rand.Next(arr.Width());
                    c = rand.Next(arr.Height());
                    d = rand.Next(arr.Height());
                    temp = arr[a, c];
                    arr[a, c] = arr[b, d];
                    arr[b, d] = temp;
                }
            
        }
    }
}

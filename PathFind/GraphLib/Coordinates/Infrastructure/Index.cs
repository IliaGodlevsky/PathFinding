using GraphLib.Coordinates.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Coordinates.Infrastructure
{
    public static class Index
    {
        private static int GetMultiplication(int first, int second)
        {
            return first * second;
        }

        public static int ToIndex(ICoordinate coordinate, params int[] dimensions)
        {
            if (coordinate.Coordinates.Count() != dimensions.Length + 1)
            {
                throw new ArgumentException("Not enough arguments");
            }

            var coordinates = coordinate.Coordinates.ToArray();
            int index = coordinates.Last();

            for (int i = 0; i < dimensions.Length - 1; i++)
            {
                for (int j = 1 + i; j < dimensions.Length; j++)
                {
                    dimensions[i] *= dimensions[j];
                }
            }

            return dimensions.Zip(coordinates, GetMultiplication).Sum() + index;
        }

        public static IEnumerable<int> ToCoordinate(int currentIndex,
            params int[] dimensions)
        {
            if (currentIndex >= dimensions.Aggregate(GetMultiplication) || currentIndex < 0)
            {
                throw new ArgumentOutOfRangeException("Index is out of range");
            }

            for (int i = 0; i < dimensions.Length; i++)
            {
                int coordinate = currentIndex % dimensions[i];
                currentIndex /= dimensions[i];

                yield return coordinate;
            }
        }
    }
}

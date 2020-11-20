using GraphLib.Coordinates.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Coordinates
{
    public static class Index
    {
        public static int ToIndex(ICoordinate coordinate, params int[] referenceDimensions)
        {
            if (coordinate.Coordinates.Count() != referenceDimensions.Length + 1)
            {
                throw new ArgumentException("Not enough arguments");
            }

            var coordinates = coordinate.Coordinates.ToArray();
            int index = coordinates.Last();

            for (int i = 0; i < referenceDimensions.Length - 1; i++)
            {
                for (int j = 1; j < referenceDimensions.Length; j++)
                {
                    referenceDimensions[i] *= referenceDimensions[j];
                }
            }

            for (int i = 0; i < referenceDimensions.Length; i++)
            {
                index += referenceDimensions[i] * coordinates[i];
            }

            return index;
        }

        public static IEnumerable<int> ToCoordinate(int currentIndex,
            params int[] referenceDimensions)
        {
            if (currentIndex >= referenceDimensions.Aggregate((x, y) => x * y) || currentIndex < 0) 
            {
                throw new ArgumentOutOfRangeException("Index is out of range");
            }

            for (int i = 0; i < referenceDimensions.Length; i++)
            {
                int coordinate = currentIndex % referenceDimensions[i];
                currentIndex /= referenceDimensions[i];

                yield return coordinate;
            }
        }
    }
}

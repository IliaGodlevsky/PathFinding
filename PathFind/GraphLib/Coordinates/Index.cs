using GraphLib.Coordinates.Interface;
using System;
using System.Linq;

namespace GraphLib.Coordinates
{
    public static class Index
    {
        public static int ToIndex(ICoordinate coordinate, params int[] referenceDimensions)
        {
            for (int i = 0; i < referenceDimensions.Length - 1; i++)
                for (int j = 1; j < referenceDimensions.Length; j++)
                    referenceDimensions[i] *= referenceDimensions[j];
            if (coordinate.Coordinates.Count() != referenceDimensions.Length + 1)
                throw new ArgumentException("Not enough arguments");
            var coordinates = coordinate.Coordinates.ToArray();
            int index = coordinates.Last();
            for (int i = 0; i < referenceDimensions.Length; i++)            
                index += referenceDimensions[i] * coordinates[i];
            return index;
        }
    }
}

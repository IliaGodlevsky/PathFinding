using GraphLib.Exceptions;
using System.Linq;

namespace GraphLib.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsCardinal(this int[] coordinates, int[] neighbourCoordinates)
        {
            if (coordinates.Length != neighbourCoordinates.Length)
            {
                throw new WrongNumberOfDimensionsException();
            }

            bool IsSubNotZero(int i) => coordinates[i] - neighbourCoordinates[i] != 0;
            return Enumerable.Range(0, coordinates.Length).Count(IsSubNotZero) == 1;
        }
    }
}

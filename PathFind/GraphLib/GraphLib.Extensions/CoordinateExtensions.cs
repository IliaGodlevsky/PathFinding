using GraphLib.Interfaces;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class CoordinateExtensions
    {
        /// <summary>
        /// Compares two coordinates
        /// </summary>
        /// <param name="self"></param>
        /// <param name="coordinate"></param>
        /// <returns>true if all of the coordinates values of <paramref name="self"/> 
        /// equals to the corresponding coordinates of <paramref name="coordinate"/>;
        /// false if not, or if they have not equal number of coordinates values
        /// or any of parametres is null</returns>
        public static bool IsEqual(this ICoordinate self, ICoordinate coordinate)
        {
            #region InvariantsObservance
            if (self == null || coordinate == null)
            {
                return false;
            }
            #endregion

            return self.CoordinatesValues.SequenceEqual(coordinate.CoordinatesValues);
        }
    }
}

using System.Collections.Generic;

namespace GraphLib.Comparers
{
    internal class DimensionComparer : IEqualityComparer<int>
    {
        /// <summary>
        /// Checks whether <paramref name="coordinate"/> is lesser
        /// than <paramref name="graphDimension"/> and greater or equals 0
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="graphDimension"></param>
        /// <returns>true if <paramref name="coordinate"/> lesser 
        /// than <paramref name="graphDimension"/> and if <paramref name="coordinate"/> 
        /// greater or equal than 0 and false if not</returns>
        public bool Equals(int coordinate, int graphDimension)
        {
            return coordinate < graphDimension && coordinate >= 0;
        }

        public int GetHashCode(int obj)
        {
            return obj.GetHashCode();
        }
    }
}

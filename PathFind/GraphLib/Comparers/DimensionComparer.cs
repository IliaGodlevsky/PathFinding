using System.Collections.Generic;

namespace GraphLib.Comparers
{
    internal class DimensionComparer : IEqualityComparer<int>
    {
        public bool Equals(int coordinate, int graphDimension)
        {
            return coordinate < graphDimension && coordinate > 0;
        }

        public int GetHashCode(int obj)
        {
            return obj.GetHashCode();
        }
    }
}

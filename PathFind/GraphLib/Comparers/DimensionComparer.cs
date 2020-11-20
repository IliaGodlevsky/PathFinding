using System.Collections.Generic;

namespace GraphLib.Comparers
{
    internal class DimensionComparer : IEqualityComparer<int>
    {
        public bool Equals(int coordinate, int graphDimension)
        {
            return coordinate < graphDimension;
        }

        public int GetHashCode(int obj)
        {
            return obj.GetHashCode();
        }
    }
}

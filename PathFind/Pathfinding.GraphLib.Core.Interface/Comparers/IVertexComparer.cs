using Shared.Primitives.Single;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Interface.Comparers
{
    public sealed class VertexCoordinateComparer 
        : Singleton<VertexCoordinateComparer, IComparer<IVertex>>, IComparer<IVertex>
    {
        private VertexCoordinateComparer()
        {

        }

        public int Compare(IVertex x, IVertex y)
        {
            if (x.Position.SequenceEqual(y.Position))
            {
                return 0;
            }
            if (IsGreater(x.Position, y.Position))
            {
                return 1;
            }

            return -1;
        }

        private static bool IsGreater(ICoordinate x, ICoordinate y)
        {
            for (int i = 0; i < x.Count; i++)
            {
                if (x[i] > y[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}

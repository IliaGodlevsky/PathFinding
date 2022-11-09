using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Coordinates;
using Pathfinding.GraphLib.Factory.Interface;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Factory.Realizations.CoordinateFactories
{
    public sealed class Coordinate2DFactory : ICoordinateFactory
    {
        public ICoordinate CreateCoordinate(IReadOnlyList<int> coordinates)
        {
            return new Coordinate2D(coordinates);
        }
    }
}

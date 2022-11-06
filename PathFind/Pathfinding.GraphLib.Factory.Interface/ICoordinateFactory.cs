using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Factory.Interface
{
    public interface ICoordinateFactory
    {
        ICoordinate CreateCoordinate(IReadOnlyList<int> coordinates);
    }
}

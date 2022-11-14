using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Factory.Interface
{
    public interface IPathfindingRangeFactory
    {
        IPathfindingRange CreateRange(IVertex source, IVertex target, IEnumerable<IVertex> intermediates);
    }
}

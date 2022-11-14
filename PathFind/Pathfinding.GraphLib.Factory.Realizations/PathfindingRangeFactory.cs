using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Interface;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Factory.Realizations
{
    public sealed class PathfindingRangeFactory : IPathfindingRangeFactory
    {
        public IPathfindingRange CreateRange(IVertex source, IVertex target, IEnumerable<IVertex> intermediates)
        {
            return new PathfindingRange(source, target, intermediates);
        }
    }
}

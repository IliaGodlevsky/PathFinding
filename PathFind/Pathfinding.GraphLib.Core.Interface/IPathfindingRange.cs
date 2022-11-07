using System.Collections.Generic;

namespace Pathfinding.GraphLib.Core.Interface
{
    public interface IPathfindingRange : IEnumerable<IVertex>
    {
        IVertex Target { get; }

        IVertex Source { get; }
    }
}
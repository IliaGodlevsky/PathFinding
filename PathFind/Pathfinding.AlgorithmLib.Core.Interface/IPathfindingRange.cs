using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Interface
{
    public interface IPathfindingRange : IEnumerable<IVertex>
    {
        IVertex Target { get; }

        IVertex Source { get; }
    }
}
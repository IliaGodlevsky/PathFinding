using Pathfinding.GraphLib.Core.Interface;
using System.Collections.Generic;

namespace Pathfinding.VisualizationLib.Core.Interface
{
    public interface IPathfindingRangeAdapter<TVertex>
        where TVertex : IVertex, IVisualizable
    {
        TVertex Source { get; }

        TVertex Target { get; }

        IReadOnlyCollection<TVertex> Intermediates { get; }

        IPathfindingRange GetPathfindingRange();
    }
}

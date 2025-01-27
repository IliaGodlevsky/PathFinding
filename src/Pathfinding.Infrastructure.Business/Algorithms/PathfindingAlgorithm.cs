using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class PathfindingAlgorithm<TStorage> : PathfindingProcess
        where TStorage : new()
    {
        protected readonly HashSet<IPathfindingVertex> visited = new();
        protected readonly Dictionary<Coordinate, IPathfindingVertex> traces = new();
        protected readonly TStorage storage = new();

        protected (IPathfindingVertex Source, IPathfindingVertex Target) CurrentRange { get; set; }

        protected IPathfindingVertex CurrentVertex { get; set; } = NullPathfindingVertex.Interface;

        protected PathfindingAlgorithm(IEnumerable<IPathfindingVertex> pathfindingRange)
            : base(pathfindingRange)
        {
            CurrentRange = (NullPathfindingVertex.Interface, NullPathfindingVertex.Interface);
        }

        protected override bool IsDestination()
        {
            return CurrentVertex.Equals(CurrentRange.Target);
        }

        protected override void PrepareForSubPathfinding(
            (IPathfindingVertex Source, IPathfindingVertex Target) range)
        {
            CurrentRange = range;
            CurrentVertex = CurrentRange.Source;
        }

        protected override IGraphPath GetSubPath()
        {
            return new GraphPath(traces.ToImmutableDictionary(),
                CurrentRange.Target);
        }

        protected override void DropState()
        {
            visited.Clear();
            traces.Clear();
        }

        protected virtual IReadOnlyCollection<IPathfindingVertex> GetUnvisitedNeighbours(
            IPathfindingVertex vertex)
        {
            return vertex.Neighbors
                .Where(v => !v.IsObstacle && !visited.Contains(v))
                .ToArray();
        }
    }
}
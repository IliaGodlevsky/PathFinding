using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Comparers;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class PathfindingAlgorithm<TStorage> : PathfindingProcess
        where TStorage : new()
    {
        protected readonly HashSet<IVertex> visited = new();
        protected readonly Dictionary<Coordinate, IVertex> traces = new();
        protected readonly TStorage storage = new();

        protected (IVertex Source, IVertex Target) CurrentRange { get; set; }

        protected IVertex CurrentVertex { get; set; } = NullVertex.Interface;

        protected PathfindingAlgorithm(IEnumerable<IVertex> pathfindingRange)
            : base(pathfindingRange)
        {
            CurrentRange = (NullVertex.Interface, NullVertex.Interface);
        }

        protected override bool IsDestination()
        {
            return CurrentVertex.Equals(CurrentRange.Target);
        }

        protected override void PrepareForSubPathfinding((IVertex Source, IVertex Target) range)
        {
            CurrentRange = range;
            CurrentVertex = CurrentRange.Source;
        }

        protected override IGraphPath GetSubPath()
        {
            return new GraphPath(traces.ToDictionary(), CurrentRange.Target);
        }

        protected override void DropState()
        {
            visited.Clear();
            traces.Clear();
        }

        protected virtual IReadOnlyCollection<IVertex> GetUnvisitedNeighbours(IVertex vertex)
        {
            return vertex.Neighbours
                .Where(v => !v.IsObstacle && !visited.Contains(v))
                .ToArray();
        }
    }
}
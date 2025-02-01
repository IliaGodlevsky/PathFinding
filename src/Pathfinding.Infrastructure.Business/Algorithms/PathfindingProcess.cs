using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Service.Interface;

namespace Pathfinding.Infrastructure.Business.Algorithms
{
    public abstract class PathfindingProcess(IEnumerable<IPathfindingVertex> pathfindingRange) 
        : IAlgorithm<IGraphPath>
    {
        public event VertexProcessedEventHandler VertexProcessed;
        public event SubPathFoundEventHandler SubPathFound;

        public virtual IGraphPath FindPath()
        {
            var subPaths = new List<IGraphPath>();
            foreach (var range in GetSubRanges())
            {
                PrepareForSubPathfinding(range);
                while (!IsDestination())
                {
                    InspectCurrentVertex();
                    MoveNextVertex();
                    VisitCurrentVertex();
                }
                var subPath = GetSubPath();
                subPaths.Add(subPath);
                RaiseSubPathFound(subPath);
                DropState();
            }
            return CreatePath(subPaths);
        }

        protected abstract void MoveNextVertex();

        protected abstract void InspectCurrentVertex();

        protected abstract void VisitCurrentVertex();

        protected abstract bool IsDestination();

        protected abstract void DropState();

        protected abstract void PrepareForSubPathfinding(
            (IPathfindingVertex Source, IPathfindingVertex Target) range);

        protected abstract IGraphPath GetSubPath();

        protected void RaiseVertexProcessed(IPathfindingVertex vertex,
            IEnumerable<IPathfindingVertex> vertices)
        {
            VertexProcessed?.Invoke(this, new(vertex, vertices));
        }

        protected void RaiseSubPathFound(IGraphPath subPath)
        {
            SubPathFound?.Invoke(this, new(subPath));
        }

        private IEnumerable<(IPathfindingVertex Source, IPathfindingVertex Target)> GetSubRanges()
        {
            using var iterator = pathfindingRange.GetEnumerator();
            if (iterator.MoveNext())
            {
                var previous = iterator.Current;
                while (iterator.MoveNext())
                {
                    var current = iterator.Current;
                    yield return (previous, current);
                    previous = iterator.Current;
                }
            }
        }

        private static IGraphPath CreatePath(List<IGraphPath> subPaths)
        {
            return subPaths.Count switch
            {
                1 => subPaths[0],
                > 1 => new CompositeGraphPath(subPaths),
                _ => NullGraphPath.Instance
            };
        }
    }
}
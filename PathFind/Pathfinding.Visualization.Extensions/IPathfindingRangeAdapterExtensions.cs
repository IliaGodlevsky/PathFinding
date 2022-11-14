using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Visualization.Extensions
{
    public static class IPathfindingRangeAdapterExtensions
    {
        public static IEnumerable<TVertex> GetRange<TVertex>(this IPathfindingRangeAdapter<TVertex> adapter)
            where TVertex : IVertex, IVisualizable
        {
            return adapter.Intermediates.Append(adapter.Source).Append(adapter.Target);
        }

        public static bool IsInRange<TVertex>(this IPathfindingRangeAdapter<TVertex> adapter, TVertex vertex)
            where TVertex : IVertex, IVisualizable
        {
            return adapter.GetRange().Contains(vertex);
        }

        public static bool IsIntermediate<TVertex>(this IPathfindingRangeAdapter<TVertex> adapter, TVertex vertex)
            where TVertex : IVertex, IVisualizable
        {
            return adapter.Intermediates.Any(intermediate => intermediate.Equals(vertex));
        }

        public static bool CanBeInRange<TVertex>(this IPathfindingRangeAdapter<TVertex> adapter, TVertex vertex)
            where TVertex : IVertex, IVisualizable
        {
            return !vertex.IsIsolated() && !adapter.IsInRange(vertex);
        }

        public static bool HasSourceAndTargetSet<TVertex>(this IPathfindingRangeAdapter<TVertex> adapter)
            where TVertex : IVertex, IVisualizable
        {
            return adapter.Source?.IsIsolated() == false && adapter.Target?.IsIsolated() == false;
        }
    }
}

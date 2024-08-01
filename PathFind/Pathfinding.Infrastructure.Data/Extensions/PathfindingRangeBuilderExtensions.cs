using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Commands;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Data.Extensions
{
    public static class PathfindingRangeBuilderExtensions
    {
        public static void Include<TVertex>(this IPathfindingRangeBuilder<TVertex> builder,
            IEnumerable<ICoordinate> coordinates, IGraph<TVertex> graph)
            where TVertex : IVertex
        {
            var pathfindingRange = coordinates.ToList();
            if (pathfindingRange.Count > 2)
            {
                var target = pathfindingRange[pathfindingRange.Count - 1];
                pathfindingRange.RemoveAt(pathfindingRange.Count - 1);
                pathfindingRange.Insert(1, target);
            }
            pathfindingRange.Select(graph.Get).ForEach(builder.Include);
        }
    }
}

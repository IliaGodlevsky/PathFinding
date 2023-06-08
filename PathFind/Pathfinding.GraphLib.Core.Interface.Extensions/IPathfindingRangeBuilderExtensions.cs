using Pathfinding.GraphLib.Core.Modules.Interface;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Core.Interface.Extensions
{
    public static class IPathfindingRangeBuilderExtensions
    {
        public static void Include<TVertex>(this IPathfindingRangeBuilder<TVertex> builder, 
            IEnumerable<ICoordinate> coordinates, IGraph<TVertex> graph)
            where TVertex : IVertex
        {
            foreach (var coordinate in coordinates)
            {
                var vertex = graph.Get(coordinate);
                builder.Include(vertex);
            }
        }
    }
}

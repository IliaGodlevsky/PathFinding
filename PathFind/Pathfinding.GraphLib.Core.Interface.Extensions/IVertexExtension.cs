using Pathfinding.GraphLib.Core.NullObjects;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Interface.Extensions
{
    public static class IVertexExtension
    {
        public static bool IsIsolated(this IVertex self)
        {
            return self?.IsObstacle == true
                || self?.Neighbours.Keys.All(vertex => vertex.IsObstacle) == true;
        }

        public static bool HasNoNeighbours(this IVertex vertex)
        {
            return vertex.Neighbours.Count == 0;
        }

        public static void InitializeComponents(this IVertex self)
        {
            self.Neighbours = new Dictionary<IVertex, IVertexCost>();
        }

        public static bool IsEqual(this IVertex self, IVertex vertex)
        {
            return self.Position.Equals(vertex.Position)
                && self.IsObstacle == vertex.IsObstacle;
        }
    }
}
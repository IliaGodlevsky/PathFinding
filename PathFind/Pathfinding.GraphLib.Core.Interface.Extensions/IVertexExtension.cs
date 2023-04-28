using Pathfinding.GraphLib.Core.NullObjects;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Core.Interface.Extensions
{
    public static class IVertexExtension
    {
        public static bool IsIsolated(this IVertex self)
        {
            return self?.IsObstacle == true
                || self?.Neighbours.All(vertex => vertex.IsObstacle) == true;
        }

        public static bool HasNoNeighbours(this IVertex vertex)
        {
            return vertex.Neighbours.Count == 0;
        }

        public static void InitializeComponents(this IVertex self)
        {
            self.Cost = NullCost.Interface;
            self.Neighbours = new List<IVertex>();
        }

        public static bool IsEqual(this IVertex self, IVertex vertex)
        {
            if (!ReferenceEquals(self, vertex))
            {
                bool hasEqualCost = self.Cost.Equals(vertex.Cost);
                bool hasEqualPosition = self.Position.Equals(vertex.Position);
                bool hasSameObstacleStatus = self.IsObstacle == vertex.IsObstacle;
                return hasEqualCost && hasEqualPosition && hasSameObstacleStatus;
            }
            return true;
        }
    }
}
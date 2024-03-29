﻿using System.Linq;

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

        public static bool IsEqual(this IVertex self, IVertex vertex)
        {
            return self.Cost.Equals(vertex.Cost)
                && self.Position.Equals(vertex.Position)
                && self.IsObstacle == vertex.IsObstacle;
        }
    }
}
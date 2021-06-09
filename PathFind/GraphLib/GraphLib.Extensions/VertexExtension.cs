using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class VertexExtension
    {
        public static string GetInforamtion(this IVertex self)
        {
            string cost = self.Cost.CurrentCost.ToString();
            string position = self.Position.ToString();
            return $"[Cost: {cost}; position: {position}]";
        }

        public static bool IsIsolated(this IVertex self)
        {
            bool IsObstacleOrNullObject(IVertex vertex)
            {
                return vertex.IsObstacle || vertex.IsNullObject();
            }

            return IsObstacleOrNullObject(self) || self.Neighbours.All(IsObstacleOrNullObject);
        }

        /// <summary>
        /// Returns vertex to its start state
        /// </summary>
        /// <param name="self"></param>
        public static void SetToDefault(this IVertex self)
        {
            if (self is IMarkable vertex)
            {
                vertex.MarkAsRegular();
            }
        }

        public static bool IsCardinal(this IVertex vertex, IVertex neighbour)
        {
            return vertex.Position.IsCardinal(neighbour.Position);
        }

        public static bool IsClose(this IVertex vertex, IVertex neighbour)
        {
            return vertex.Position.IsClose(neighbour.Position);
        }

        public static void Initialize(this IVertex self)
        {
            self.Neighbours = new List<IVertex>();
            self.IsObstacle = false;
            self.Cost = new NullCost();
            self.SetToDefault();
        }

        internal static void Refresh(this IVertex self)
        {
            if (!self.IsObstacle)
            {
                self.SetToDefault();
            }
        }

        public static bool IsEqual(this IVertex self, IVertex vertex)
        {
            bool hasEqualCost = self.Cost.Equals(vertex.Cost);
            bool hasEqualPosition = self.Position.Equals(vertex.Position);
            bool hasSameObstacleStatus = self.IsObstacle == vertex.IsObstacle;
            return hasEqualCost && hasEqualPosition && hasSameObstacleStatus;
        }

        /// <summary>
        /// Sets certain vertices of <paramref name="self"/>'s 
        /// environment as its neighbors
        /// </summary>
        /// <param name="self"></param>
        /// <param name="graph">A graph, where vertex is situated</param>
        /// <exception cref="ArgumentNullException">Thrown when
        /// any of parametre is empty</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="graph"/> 
        /// doesn't contain <paramref name="self"/></exception>
        public static void SetNeighbours(this IVertex self, IGraph graph)
        {
            if (self is null)
            {
                throw new ArgumentNullException(nameof(self));
            }
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }
            if (!graph.Contains(self))
            {
                throw new ArgumentException("Vertex doesn't belong to graph\n", nameof(self));
            }

            self.Neighbours = self
                .NeighboursCoordinates
                .Coordinates
                .Where(coordinate => coordinate.IsWithinGraph(graph))
                .Select(coordinate => graph[coordinate])
                .Where(vertex => graph.CanBeNeighbours(vertex, self))
                .ToList();
        }
    }
}
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using NullObject.Extensions;
using System;
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
            bool IsObstacleOrNull(IVertex vertex)
            {
                return vertex.IsObstacle || vertex.IsNull();
            }

            return IsObstacleOrNull(self) || self.Neighbours.All(IsObstacleOrNull);
        }

        public static bool IsNeighbour(this IVertex self, IVertex candidate)
        {
            bool IsAtSamePosition(IVertex vertex)
            {
                return vertex.Position.IsEqual(candidate.Position)
                    && ReferenceEquals(vertex, candidate);
            }
            return self.Neighbours.Any(IsAtSamePosition);
        }

        public static int[] GetCoordinates(this IVertex self)
        {
            return self.Position.CoordinatesValues.ToArray();
        }

        public static bool CanBeNeighbour(this IVertex self, IVertex candidate)
        {
            return !ReferenceEquals(candidate, self) && !self.IsNeighbour(candidate);
        }

        /// <summary>
        /// Returns vertex to its start state
        /// </summary>
        /// <param name="self"></param>
        public static void SetToDefault(this IVertex self)
        {
            if (self is IVisualizable vertex)
            {
                vertex.VisualizeAsRegular();
            }
        }

        public static void Initialize(this IVertex self)
        {
            self.Neighbours = new IVertex[] { };
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
        /// any of parametre is null</exception>
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
                .Where(vertex => self.CanBeNeighbour(vertex))
                .ToList();
        }
    }
}
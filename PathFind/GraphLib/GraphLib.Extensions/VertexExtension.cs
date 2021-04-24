using Common.Extensions;
using GraphLib.Common.NullObjects;
using GraphLib.Interfaces;
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
            string neighboursCount = self.Neighbours.Count.ToString();
            return $"Cost: {cost}; position: {position}; neighbours: {neighboursCount}";
        }

        public static bool IsIsolated(this IVertex self)
        {
            return self.IsObstacle
                || self.Neighbours.All(neighbour => neighbour.IsObstacle);
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

        public static void Initialize(this IVertex self)
        {
            self.Neighbours = new List<IVertex>();
            self.SetToDefault();
            self.IsObstacle = false;
            self.Position = new DefaultCoordinate();
            self.Cost = new DefaultCost();
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
            #region InvariantsObservance

            var message = "An error was occured while setting vertices neighbours\n";
            if (graph == null)
            {
                message += "Argument was null\n";
                throw new ArgumentNullException(nameof(graph), message);
            }

            if (!graph.Contains(self))
            {
                message += "Vertex doesn't belong to graph\n";
                throw new ArgumentException(message, nameof(graph));
            }

            if (!graph.Vertices.Any())
            {
                return;
            }

            #endregion

            ICoordinate Coordinate(IEnumerable<int> coordinates) => new Coordinate(coordinates);
            IVertex Vertex(ICoordinate coordinate) => graph[coordinate];
            bool IsWithingGraph(ICoordinate coordinate) => coordinate.IsWithinGraph(graph);
            bool CanBeNeighbour(IVertex vertex) => graph.CanBeNeighbourOf(vertex, self);

            self.Neighbours = self
                .CoordinateRadar
                .Environment
                .Select(Coordinate)
                .Which(IsWithingGraph)
                .Select(Vertex)
                .Which(CanBeNeighbour)
                .ToList();
        }
    }
}
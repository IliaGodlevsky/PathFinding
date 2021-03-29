using Common.Extensions;
using GraphLib.Common;
using GraphLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class IVertexExtension
    {
        public static bool IsValidToBeEndPoint(this IVertex self)
        {
            return !self.IsIsolated();
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
            bool areObstacles = self.IsObstacle == vertex.IsObstacle;
            return hasEqualCost && hasEqualPosition && areObstacles;
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
            #endregion

            bool IsWithingGraph(int[] coordinate) => coordinate.IsWithinGraph(graph);
            IVertex Vertex(int[] coordinate) => graph[coordinate];
            bool CanBeNeighbourOf(IVertex vertex) => vertex.CanBeNeighbourOf(self);

            if (graph.Vertices.Any())
            {
                var coordinateRadar = new CoordinateAroundRadar(self.Position);
                self.Neighbours = coordinateRadar
                                    .Environment
                                    .Which(IsWithingGraph)
                                    .Select(Vertex)
                                    .Which(CanBeNeighbourOf)
                                    .ToList();
            }
        }

        public static bool CanBeNeighbourOf(this IVertex self, IVertex vertex)
        {
            return !ReferenceEquals(vertex, self) && !self.IsNeighbourOf(vertex);
        }

        public static bool IsNeighbourOf(this IVertex self, IVertex vertex)
        {
            return vertex.Neighbours.Contains(self);
        }
    }
}
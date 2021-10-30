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
        /// <summary>
        /// Returns the information about <paramref name="self"/> in format 
        /// [Cost: <see cref="IVertex.Cost"/>; position: <see cref="IVertex.Position"/>]
        /// </summary>
        /// <param name="self"></param>
        /// <returns>The info about <paramref name="self"/> in format </returns>
        public static string GetInforamtion(this IVertex self)
        {
            string cost = self.Cost.CurrentCost.ToString();
            string position = self.Position.ToString();
            return $"[Cost: {cost}; position: {position}]";
        }

        /// <summary>
        /// Determins, whether <paramref name="self"/> is isolated, 
        /// e.a. all of its neighbours are obstacles or <see cref="NullVertex"/>
        /// or <paramref name="self"/> is obstacle or <see cref="NullVertex"/>
        /// </summary>
        /// <param name="self"></param>
        /// <returns>true if obstacle is isolated</returns>
        public static bool IsIsolated(this IVertex self)
        {
            bool IsObstacleOrNull(IVertex vertex)
            {
                return vertex.IsObstacle || vertex.IsNull();
            }

            return IsObstacleOrNull(self) || self.Neighbours.All(IsObstacleOrNull);
        }

        /// <summary>
        /// Determins, whether <paramref name="candidate"/> 
        /// is a neighbour of <paramref name="self"/>
        /// </summary>
        /// <param name="self"></param>
        /// <param name="candidate"></param>
        /// <returns><see cref="true"/> if <paramref name="candidate"/> 
        /// is a neighbour of <paramref name="self"/>, 
        /// otherwise - <see cref="false"/></returns>
        public static bool IsNeighbour(this IVertex self, IVertex candidate)
        {
            return self.Neighbours.Any(vertex =>
                ReferenceEquals(vertex, candidate)
                && ReferenceEquals(vertex.Graph, candidate.Graph));
        }

        /// <summary>
        /// Returns <paramref name="self"/> as 
        /// an array of <see cref="Int32"/>
        /// </summary>
        /// <param name="self"></param>
        /// <returns>Coordinates of <paramref name="self"/> as 
        /// an array of <see cref="Int32"/></returns>
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
            self.IsObstacle = false;
            self.Cost = NullCost.Instance;
            self.SetToDefault();
        }

        internal static void Refresh(this IVertex self)
        {
            if (!self.IsObstacle)
            {
                self.SetToDefault();
            }
        }

        /// <summary>
        /// Determins, whether <paramref name="self"/> 
        /// is equal to <paramref name="vertex"/>
        /// by cost, position and obstacle status
        /// </summary>
        /// <param name="self">The vertex, that is compared</param>
        /// <param name="vertex">The vertex, to 
        /// which<paramref name="self"/> is compared</param>
        /// <returns><see cref="true"/> if <paramref name="self"/> is 
        /// equal to <paramref name="vertex"/>, 
        ///  otherwise - <see cref="false"/></returns>
        public static bool IsEqual(this IVertex self, IVertex vertex)
        {
            bool hasEqualCost = self.Cost.Equals(vertex.Cost);
            bool hasEqualPosition = self.Position.Equals(vertex.Position);
            bool hasSameObstacleStatus = self.IsObstacle == vertex.IsObstacle;
            return hasEqualCost && hasEqualPosition && hasSameObstacleStatus;
        }

        /// <summary>
        /// Determins whether <paramref name="self"/> is equal 
        /// to any of <paramref name="vertices"/>
        /// </summary>
        /// <param name="self"></param>
        /// <param name="vertices">Array of vertices, to each element </param>
        /// <returns></returns>
        public static bool IsOneOf(this IVertex self, params IVertex[] vertices)
        {
            return vertices.Any(vertex => vertex.Equals(self));
        }

        /// <summary>
        /// Sets certain vertices of <paramref name="self"/>'s 
        /// environment as its neighbors
        /// </summary>
        /// <param name="self"></param>
        /// <param name="graph">A graph, where vertex is situated</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="graph"/> 
        /// doesn't contain <paramref name="self"/></exception>
        public static IReadOnlyCollection<IVertex> GetNeighbours(this IVertex self)
        {
            return self
                .NeighboursCoordinates
                .Coordinates
                .Where(coordinate => coordinate.IsWithinGraph(self.Graph))
                .Select(coordinate => self.Graph[coordinate])
                .ToArray();
        }
    }
}
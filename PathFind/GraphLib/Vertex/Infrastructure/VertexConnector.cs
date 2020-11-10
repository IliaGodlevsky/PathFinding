using GraphLib.Coordinates.Interface;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.VertexConnecting
{
    /// <summary>
    /// A class that responses for connection of vertex with neighbour vertices
    /// </summary>
    public static class VertexConnector
    {
        public static void ConnectToNeighbours(IVertex vertex)
        {
            foreach (var neigbour in vertex.Neighbours)
            {
                if (CanBeNeighbour(neigbour, vertex))
                {
                    neigbour.Neighbours.Add(vertex);
                }
            }
        }

        public static void IsolateVertex(IVertex vertex)
        {
            foreach (var neigbour in vertex.Neighbours)
            {
                neigbour.Neighbours.Remove(vertex);
            }

            vertex.Neighbours.Clear();
        }

        private static bool IsWithinGraph(IGraph graph, ICoordinate coordinates)
        {
            var dimensionSizes = graph.DimensionsSizes.ToArray();
            var currentCoordinates = coordinates.Coordinates.ToArray();

            if (dimensionSizes.Length != currentCoordinates.Length)
            {
                throw new ArgumentException("Dimensions of coordinate and graph don't match each other");
            }

            bool IsOutOfBounds(int currentCoordinate, int dimensionSize)
            {
                return currentCoordinate < 0 || currentCoordinate >= dimensionSize;
            }

            for (int i = 0; i < currentCoordinates.Length; i++)
            {
                if (IsOutOfBounds(currentCoordinates[i], dimensionSizes[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CanBeNeighbour(IVertex vertex, IVertex neighbourCandidate)
        {
            return !neighbourCandidate.IsObstacle
                && !ReferenceEquals(vertex, neighbourCandidate)
                && !vertex.Neighbours.Contains(neighbourCandidate);
        }

        private static IEnumerable<IVertex> GetVertexEnvironment(IGraph graph, IVertex vertex)
        {
            foreach (var coordinate in vertex.Position.Environment)
            {
                if (IsWithinGraph(graph, coordinate))
                {
                    yield return graph[coordinate];
                }
            }
        }

        public static void SetNeighbours(IGraph graph, IVertex vertex)
        {
            if (!vertex.IsObstacle)
            {
                foreach (var potentialNeighbor in GetVertexEnvironment(graph, vertex))
                {
                    if (CanBeNeighbour(vertex, potentialNeighbor))
                    {
                        vertex.Neighbours.Add(potentialNeighbor);
                    }
                }
            }
        }

        public static void ConnectVertices(IGraph graph)
        {
            graph.AsParallel().ForAll(vertex => SetNeighbours(graph, vertex));
        }
    }
}

using GraphLibrary.Coordinates.Interface;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary.VertexConnecting
{
    /// <summary>
    /// A class that responses for connection of vertex with neighbour vertices
    /// </summary>
    public static class VertexConnector
    {
        /// <summary>
        /// Connects vertex with its neighbours
        /// </summary>
        /// <param name="vertex"></param>
        public static void ConnectToNeighbours(IVertex vertex)
        {
            foreach (var neigbour in vertex.Neighbours)
            {
                if (CanBeNeighbour(neigbour, vertex))
                    neigbour.Neighbours.Add(vertex);
            }
        }

        /// <summary>
        /// Removes vertex connections with its neighbours
        /// </summary>
        /// <param name="vertex"></param>
        public static void IsolateVertex(IVertex vertex)
        {
            foreach (var neigbour in vertex.Neighbours)
                neigbour.Neighbours.Remove(vertex);
            vertex.Neighbours.Clear();
        }

        private static bool IsWithinGraph(IGraph graph, ICoordinate coordinates)
        {
            var dimensionSizes = graph.DimensionsSizes.ToArray();
            var currentCoordinates = coordinates.Coordinates.ToArray();
            if (dimensionSizes.Length != currentCoordinates.Length)
                return false;
            bool IsOutOfBounds(int currentCoordinate, int dimensionSize)
                => currentCoordinate < 0 || currentCoordinate >= dimensionSize;
            for (int i = 0; i < currentCoordinates.Length; i++)
                if (IsOutOfBounds(currentCoordinates[i], dimensionSizes[i]))
                    return false;
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
                if (IsWithinGraph(graph, coordinate))
                    yield return graph[coordinate];
        }

        /// <summary>
        /// Connects neighbours with its vertex
        /// </summary>
        /// <param name="graph">graph where vertex belongs</param>
        /// <param name="vertex">vertex that must be connected with its neighbours</param>
        public static void SetNeighbours(IGraph graph, IVertex vertex)
        {
            if (vertex.IsObstacle)
                return;
            foreach (var potentialNeighbor in GetVertexEnvironment(graph, vertex))
                if (CanBeNeighbour(vertex, potentialNeighbor))
                    vertex.Neighbours.Add(potentialNeighbor);
        }

        /// <summary>
        /// Connects all vertices in the graph with each other
        /// </summary>
        /// <param name="graph"></param>
        public static void ConnectVertices(IGraph graph)
        {
            graph.AsParallel().ForAll(vertex => SetNeighbours(graph, vertex));
        }
    }
}

using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex.Interface;

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

        public static bool IsWithinGraph(IGraph graph, int width, int height)
        {
            return width >= 0 && width < graph.Width
                && height >= 0 && height < graph.Height;
        }

        private static bool CanBeNeighbour(IVertex vertex, IVertex neighbourCandidate)
        {
            return !neighbourCandidate.IsObstacle
                && !ReferenceEquals(vertex, neighbourCandidate)
                && !vertex.Neighbours.Contains(neighbourCandidate);
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
            var vertexCoordinates = graph.GetIndices(vertex);
            for (int i = vertexCoordinates.X - 1; i <= vertexCoordinates.X + 1; i++)
                for (int j = vertexCoordinates.Y - 1; j <= vertexCoordinates.Y + 1; j++)
                    if (IsWithinGraph(graph, i, j) && CanBeNeighbour(vertex, graph[i, j]))
                        vertex.Neighbours.Add(graph[i, j]);
        }

        /// <summary>
        /// Connects all vertices in the graph with each other
        /// </summary>
        /// <param name="graph"></param>
        public static void ConnectVertices(IGraph graph)
        {
            graph.Array.ApplyParallel(vertex =>
            {
                SetNeighbours(graph, vertex);
                return vertex;
            });
        }
    }
}

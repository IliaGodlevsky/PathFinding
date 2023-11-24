using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class NeighbourAddedMessage
    {
        public Vertex Vertex { get; }

        public Vertex VertexNeighbour { get; }

        public NeighbourAddedMessage(Vertex vertex, Vertex vertexNeighbour)
        {
            Vertex = vertex;
            VertexNeighbour = vertexNeighbour;
        }
    }
}

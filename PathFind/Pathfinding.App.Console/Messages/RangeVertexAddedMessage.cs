using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class RangeVertexAddedMessage
    {
        public Vertex Vertex { get; }

        public int Order { get; }

        public RangeVertexAddedMessage(Vertex vertex, int order)
        {
            Vertex = vertex;
            Order = order;
        }
    }
}

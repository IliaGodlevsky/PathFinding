using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class VertexChangedMessage
    {
        public Vertex Vertex { get; }

        public VertexChangedMessage(Vertex vertex)
        {
            Vertex = vertex;
        }
    }
}

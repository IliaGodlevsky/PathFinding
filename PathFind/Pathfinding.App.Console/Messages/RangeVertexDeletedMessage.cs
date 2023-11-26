using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class RangeVertexDeletedMessage
    {
        public Vertex Vertex { get; }

        public RangeVertexDeletedMessage(Vertex vertex)
        {
            Vertex = vertex;
        }
    }
}

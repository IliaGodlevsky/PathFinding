using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class RemoveFromVisualizedMessage
    {
        public Vertex Vertex { get; }

        public RemoveFromVisualizedMessage(Vertex vertex)
        {
            Vertex = vertex;
        }
    }
}

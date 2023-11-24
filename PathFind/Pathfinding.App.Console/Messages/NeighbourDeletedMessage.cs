using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class NeighbourDeletedMessage
    {
        public Vertex Vertex { get; }

        public Vertex Neighbour { get; }

        public NeighbourDeletedMessage(Vertex vertex, Vertex neighbour)
        {
            Vertex = vertex;
            Neighbour = neighbour;
        }
    }
}

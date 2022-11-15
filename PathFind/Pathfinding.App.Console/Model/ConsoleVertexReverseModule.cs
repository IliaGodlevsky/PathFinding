using Pathfinding.GraphLib.Visualization.Subscriptions;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ConsoleVertexReverseModule : ReverseVertexModule<Vertex>
    {
        public void ReverseVertex(Vertex vertex)
        {
            Reverse(vertex);
        }
    }
}

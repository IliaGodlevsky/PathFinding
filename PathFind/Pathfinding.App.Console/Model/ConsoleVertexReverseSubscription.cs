using Pathfinding.App.Console.EventArguments;
using Pathfinding.GraphLib.Visualization.Subscriptions;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ConsoleVertexReverseSubscription : ReverseVertexSubscription<Vertex>
    {
        private void ReverseVertex(object sender, VertexEventArgs e)
        {
            Reverse(e.Current);
        }

        protected override void SubscribeVertex(Vertex vertex)
        {
            vertex.VertexReversed += ReverseVertex;
        }

        protected override void UnsubscribeVertex(Vertex vertex)
        {
            vertex.VertexReversed -= ReverseVertex;
        }
    }
}

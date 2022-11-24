using Pathfinding.App.WPF._3D.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;

namespace Pathfinding.App.WPF._3D.Messages.PassValueMessages
{
    internal sealed class GraphCreatedMessage : PassValueMessage<Graph3D<Vertex3D>>
    {
        public GraphCreatedMessage(Graph3D<Vertex3D> graph)
            : base(graph)
        {

        }
    }
}
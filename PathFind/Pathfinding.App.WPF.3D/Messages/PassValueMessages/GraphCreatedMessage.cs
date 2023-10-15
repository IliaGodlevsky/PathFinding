using Pathfinding.App.WPF._3D.Model;
using Pathfinding.GraphLib.Core.Interface;

namespace Pathfinding.App.WPF._3D.Messages.PassValueMessages
{
    internal sealed class GraphCreatedMessage : PassValueMessage<IGraph<Vertex3D>>
    {
        public GraphCreatedMessage(IGraph<Vertex3D> graph)
            : base(graph)
        {

        }
    }
}
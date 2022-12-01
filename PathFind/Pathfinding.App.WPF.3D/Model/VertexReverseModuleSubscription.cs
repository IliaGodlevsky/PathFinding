using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using System.Windows.Input;

namespace Pathfinding.App.WPF._3D.Model
{
    internal sealed class VertexReverseModuleSubscription : IGraphSubscription<Vertex3D>
    {
        private readonly IPathfindingRangeBuilder<Vertex3D> rangeBuilder;

        public VertexReverseModuleSubscription(IPathfindingRangeBuilder<Vertex3D> rangeBuilder)
        {
            this.rangeBuilder = rangeBuilder;
        }

        private void ReverseVertex(Vertex3D vertex)
        {
            if (vertex.IsObstacle)
            {
                vertex.IsObstacle = false;
                vertex.VisualizeAsRegular();
            }
            else
            {
                if (!rangeBuilder.Range.IsInRange(vertex))
                {
                    vertex.IsObstacle = true;
                }
            }
        }

        public void Subscribe(IGraph<Vertex3D> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseRightButtonDown += ReverseVertex;
            }
        }

        public void Unsubscribe(IGraph<Vertex3D> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseRightButtonDown -= ReverseVertex;
            }
        }

        private void ReverseVertex(object sender, MouseEventArgs e)
        {
            ReverseVertex((Vertex3D)e.Source);
        }
    }
}

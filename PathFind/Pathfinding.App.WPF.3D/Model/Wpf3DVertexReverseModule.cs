using Pathfinding.App.WPF._3D.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using System.Windows.Input;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class Wpf3DVertexReverseModule : IGraphSubscription<Vertex3D>
    {
        private readonly IPathfindingRange range;

        public Wpf3DVertexReverseModule(IPathfindingRange range)
        {
            this.range = range;
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
                if (!range.IsInRange(vertex))
                {
                    vertex.IsObstacle = true;
                }
            }
        }

        private void ReverseVertex(object sender, MouseButtonEventArgs e)
        {
            ReverseVertex((Vertex3D)e.Source);
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
    }
}

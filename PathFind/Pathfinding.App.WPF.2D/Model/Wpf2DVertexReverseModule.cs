using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Windows.Input;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class Wpf2DVertexReverseModule : IGraphSubscription<Vertex>
    {
        private readonly IPathfindingRangeAdapter<Vertex> adapter;

        public Wpf2DVertexReverseModule(IPathfindingRangeAdapter<Vertex> adapter)
        {
            this.adapter = adapter;
        }

        private void ReverseVertex(Vertex vertex)
        {
            if (vertex.IsObstacle)
            {
                vertex.IsObstacle = false;
                vertex.VisualizeAsRegular();
            }
            else
            {
                if (!adapter.IsInRange(vertex))
                {
                    vertex.IsObstacle = true;
                }
            }
        }

        private void ReverseVertex(object sender, MouseButtonEventArgs e)
        {
            ReverseVertex((Vertex)e.Source);
        }

        public void Subscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseRightButtonDown += ReverseVertex;
            }
        }

        public void Unsubscribe(IGraph<Vertex> graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MouseRightButtonDown -= ReverseVertex;
            }
        }
    }
}

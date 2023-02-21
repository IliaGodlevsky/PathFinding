using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Windows;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class VertexFactory : IVertexFactory<Vertex>
    {
        public VertexFactory(ITotalVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
        }

        public Vertex CreateVertex(ICoordinate coordinate)
        {
            return Application.Current.Dispatcher.Invoke(() => new Vertex(coordinate, visualization));
        }

        private readonly ITotalVisualization<Vertex> visualization;
    }
}

using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System.Windows;

namespace WPFVersion.Model
{
    internal sealed class VertexFactory : IVertexFactory<Vertex>
    {
        public VertexFactory(IVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
        }

        public Vertex CreateVertex(ICoordinate coordinate)
        {
            return Application.Current.Dispatcher.Invoke(() => new Vertex(coordinate, visualization));
        }

        private readonly IVisualization<Vertex> visualization;
    }
}

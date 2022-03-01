using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System.Windows;

namespace WPFVersion.Model
{
    internal sealed class VertexFactory : IVertexFactory
    {
        public VertexFactory(IVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
        }

        public IVertex CreateVertex(INeighborhood coordinateRadar, ICoordinate coordinate)
        {
            return Application.Current.Dispatcher.Invoke(() => new Vertex(coordinateRadar, coordinate, visualization));
        }

        private readonly IVisualization<Vertex> visualization;
    }
}

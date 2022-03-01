using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;

namespace WindowsFormsVersion.Model
{
    internal sealed class VertexFactory : IVertexFactory
    {
        public VertexFactory(IVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
        }

        public IVertex CreateVertex(INeighborhood coordinateRadar, ICoordinate coordinate)
        {
            return new Vertex(coordinateRadar, coordinate, visualization);
        }

        private readonly IVisualization<Vertex> visualization;
    }
}

using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;

namespace ConsoleVersion.Model
{
    internal sealed class VertexFactory : IVertexFactory
    {
        private readonly IVisualization<Vertex> visualization;

        public VertexFactory(IVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
        }

        public IVertex CreateVertex(INeighborhood coordinateRadar, ICoordinate coordinate)
        {
            return new Vertex(coordinateRadar, coordinate, visualization);
        }
    }
}
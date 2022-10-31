using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;

namespace ConsoleVersion.Model
{
    internal sealed class VertexFactory : IVertexFactory<Vertex>
    {
        private readonly IVisualization<Vertex> visualization;

        public VertexFactory(IVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
        }

        public Vertex CreateVertex(ICoordinate coordinate)
        {
            return new Vertex(coordinate, visualization);
        }
    }
}
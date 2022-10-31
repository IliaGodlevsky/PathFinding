using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;

namespace WindowsFormsVersion.Model
{
    internal sealed class VertexFactory : IVertexFactory<Vertex>
    {
        public VertexFactory(IVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
        }

        public Vertex CreateVertex(ICoordinate coordinate)
        {
            return new Vertex(coordinate, visualization);
        }

        private readonly IVisualization<Vertex> visualization;
    }
}

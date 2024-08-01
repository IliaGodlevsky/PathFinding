using Pathfinding.Service.Interface.Visualization;

namespace Pathfinding.App.Console.Model
{
    internal sealed class VertexFactory : IVertexFactory<Vertex>
    {
        private readonly ITotalVisualization<Vertex> visualization;

        public VertexFactory(ITotalVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
        }

        public Vertex CreateVertex(ICoordinate coordinate)
        {
            return new(coordinate, visualization);
        }
    }
}
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.App.Console.Model
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
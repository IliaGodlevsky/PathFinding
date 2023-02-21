using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.VisualizationLib.Core.Interface;

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
            return new Vertex(coordinate, visualization);
        }
    }
}
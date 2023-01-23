using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.App.Console.Model
{
    internal sealed class VertexFromInfoFactory : IVertexFromInfoFactory<Vertex>
    {
        private readonly IVisualization<Vertex> visualization;

        public VertexFromInfoFactory(IVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
        }

        public Vertex CreateFrom(VertexSerializationInfo info)
        {
            return new (info.Position, visualization)
            {
                Cost = info.Cost,
                IsObstacle = info.IsObstacle
            };
        }
    }
}
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Windows;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class VertexFromInfoFactory : IVertexFromInfoFactory<Vertex>
    {
        private readonly ITotalVisualization<Vertex> visualization;

        public VertexFromInfoFactory(ITotalVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
        }

        public Vertex CreateFrom(VertexSerializationInfo info)
        {
            return Application.Current.Dispatcher.Invoke(() =>
            {
                return new Vertex(info.Position, visualization)
                {
                    IsObstacle = info.IsObstacle,
                    Cost = info.Cost
                };
            });
        }
    }
}

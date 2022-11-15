using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Windows;

namespace Pathfinding.App.WPF._2D.Model
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
            return Application.Current.Dispatcher.Invoke(() => new Vertex(info, visualization));
        }
    }
}

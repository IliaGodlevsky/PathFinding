using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;
using System.Windows;

namespace WPFVersion.Model
{
    internal sealed class VertexFromInfoFactory : IVertexFromInfoFactory
    {
        public VertexFromInfoFactory(IVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
        }

        public IVertex CreateFrom(VertexSerializationInfo info)
        {
            return Application.Current.Dispatcher.Invoke(() => new Vertex(info, visualization));
        }

        private readonly IVisualization<Vertex> visualization;
    }
}

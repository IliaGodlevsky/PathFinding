using Pathfinding.App.WPF._3D.Interface;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using System.Windows;
using System.Windows.Threading;

namespace Pathfinding.App.WPF._3D.Model
{
    internal sealed class Vertex3DFromInfoFactory : IVertexFromInfoFactory<Vertex3D>
    {
        private static readonly Dispatcher Dispatcher = Application.Current.Dispatcher;

        private readonly IVisualization<Vertex3D> visualization;
        private readonly IModel3DFactory model3DFactory;

        public Vertex3DFromInfoFactory(IVisualization<Vertex3D> visualization)
        {
            this.visualization = visualization;
        }

        public Vertex3DFromInfoFactory(IModel3DFactory model3DFactory, IVisualization<Vertex3D> visualization)
        {
            this.model3DFactory = model3DFactory;
            this.visualization = visualization;
        }

        public Vertex3D CreateFrom(VertexSerializationInfo info)
        {
            return Dispatcher.Invoke(() => new Vertex3D(info, model3DFactory, visualization));
        }
    }
}
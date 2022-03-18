using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;
using System.Windows;
using System.Windows.Threading;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model
{
    internal sealed class Vertex3DFromInfoFactory : IVertexFromInfoFactory
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

        public IVertex CreateFrom(VertexSerializationInfo info)
        {
            return Application.Current.Dispatcher.Invoke(() => new Vertex3D(info, model3DFactory, visualization));
        }
    }
}
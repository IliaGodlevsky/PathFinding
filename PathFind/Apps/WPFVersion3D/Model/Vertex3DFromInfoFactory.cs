using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;
using System.Windows;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model
{
    internal sealed class Vertex3DFromInfoFactory : IVertexFromInfoFactory
    {
        public Vertex3DFromInfoFactory(IModel3DFactory model3DFactory)
        {
            this.model3DFactory = model3DFactory;
        }

        public IVertex CreateFrom(VertexSerializationInfo info)
        {
            IVertex vertex = new NullVertex();
            Application.Current.Dispatcher.Invoke(() => vertex = new Vertex3D(info, model3DFactory));
            return vertex;
        }

        private readonly IModel3DFactory model3DFactory;
    }
}

using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;
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
            return new Vertex3D(info, model3DFactory);
        }

        private readonly IModel3DFactory model3DFactory;
    }
}

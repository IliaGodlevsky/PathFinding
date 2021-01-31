using GraphLib.Vertex.Infrastructure.Factories.Interfaces;
using GraphLib.Vertex.Interface;

namespace WPFVersion3D.Model
{
    internal class Vertex3DFactory : IVertexFactory
    {
        public IVertex CreateVertex()
        {
            return new Vertex3D();
        }
    }
}

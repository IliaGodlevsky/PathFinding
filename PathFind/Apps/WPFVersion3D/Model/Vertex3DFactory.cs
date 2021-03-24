using GraphLib.Interface;

namespace WPFVersion3D.Model
{
    internal sealed class Vertex3DFactory : IVertexFactory
    {
        public IVertex CreateVertex()
        {
            return new Vertex3D();
        }
    }
}

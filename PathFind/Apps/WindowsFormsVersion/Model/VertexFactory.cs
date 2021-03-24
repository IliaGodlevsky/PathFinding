using GraphLib.Interface;

namespace WindowsFormsVersion.Model
{
    internal sealed class VertexFactory : IVertexFactory
    {
        public IVertex CreateVertex()
        {
            return new Vertex();
        }
    }
}

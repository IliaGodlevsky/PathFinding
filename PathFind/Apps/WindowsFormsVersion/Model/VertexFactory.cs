using GraphLib.Interfaces;

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

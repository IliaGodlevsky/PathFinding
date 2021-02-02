using GraphLib.Interface;

namespace WindowsFormsVersion.Model
{
    internal class VertexFactory : IVertexFactory
    {
        public IVertex CreateVertex()
        {
            return new Vertex();
        }
    }
}

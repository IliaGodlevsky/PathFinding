using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;

namespace WindowsFormsVersion.Model
{
    internal sealed class VertexFromInfoFactory : IVertexFromInfoFactory
    {
        public VertexFromInfoFactory(IVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
        }

        public IVertex CreateFrom(VertexSerializationInfo info)
        {
            return new Vertex(info, visualization);
        }

        private readonly IVisualization<Vertex> visualization;
    }
}

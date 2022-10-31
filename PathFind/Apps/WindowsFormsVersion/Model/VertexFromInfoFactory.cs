using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;

namespace WindowsFormsVersion.Model
{
    internal sealed class VertexFromInfoFactory : IVertexFromInfoFactory<Vertex>
    {
        private readonly IVisualization<Vertex> visualization;

        public VertexFromInfoFactory(IVisualization<Vertex> visualization)
        {
            this.visualization = visualization;
        }

        public Vertex CreateFrom(VertexSerializationInfo info)
        {
            return new Vertex(info, visualization);
        }
    }
}

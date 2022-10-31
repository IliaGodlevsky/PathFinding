using GraphLib.Realizations.Graphs;
using WPFVersion3D.Model;

namespace WPFVersion3D.Messages.PassValueMessages
{
    internal sealed class GraphCreatedMessage : PassValueMessage<Graph3D<Vertex3D>>
    {
        public GraphCreatedMessage(Graph3D<Vertex3D> graph)
            : base(graph)
        {

        }
    }
}
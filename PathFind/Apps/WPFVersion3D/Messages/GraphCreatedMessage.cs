using GraphLib.Interfaces;

namespace WPFVersion3D.Messages
{
    internal sealed class GraphCreatedMessage : PassValueMessage<IGraph>
    {
        public GraphCreatedMessage(IGraph graph)
            : base(graph)
        {

        }
    }
}
using GraphLib.Interfaces;

namespace WPFVersion3D.Messages.PassValueMessages
{
    internal sealed class GraphCreatedMessage : PassValueMessage<IGraph>
    {
        public GraphCreatedMessage(IGraph graph)
            : base(graph)
        {

        }
    }
}
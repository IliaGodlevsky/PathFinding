using GraphLib.Interfaces;

namespace WPFVersion.Messages.DataMessages
{
    internal sealed class GraphFieldCreatedMessage
    {
        public IGraphField Field { get; }

        public GraphFieldCreatedMessage(IGraphField field)
        {
            Field = field;
        }
    }
}

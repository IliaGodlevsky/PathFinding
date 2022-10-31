using WPFVersion3D.Model;

namespace WPFVersion3D.Messages.PassValueMessages
{
    internal sealed class GraphFieldCreatedMessage : PassValueMessage<GraphField3D>
    {
        public GraphFieldCreatedMessage(GraphField3D field)
            : base(field)
        {

        }
    }
}
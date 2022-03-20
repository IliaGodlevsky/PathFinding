using GraphLib.Interfaces;
using WPFVersion3D.Model;

namespace WPFVersion3D.Messages
{
    internal sealed class GraphFieldCreatedMessage : PassValueMessage<GraphField3D>
    {
        public GraphFieldCreatedMessage(IGraphField field)
            : base((GraphField3D)field)
        {

        }
    }
}
using Pathfinding.App.WPF._3D.Model;

namespace Pathfinding.App.WPF._3D.Messages.PassValueMessages
{
    internal sealed class GraphFieldCreatedMessage : PassValueMessage<GraphField3D>
    {
        public GraphFieldCreatedMessage(GraphField3D field)
            : base(field)
        {

        }
    }
}
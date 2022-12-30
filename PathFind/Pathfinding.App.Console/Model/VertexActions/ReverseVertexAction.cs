using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;

namespace Pathfinding.App.Console.Model.VertexActions
{
    internal sealed class ReverseVertexAction : IVertexAction
    {
        private readonly IPathfindingRange<Vertex> range;
        private readonly IMessenger messenger;

        public ReverseVertexAction(IPathfindingRange<Vertex> range,
            IMessenger messenger)
        {
            this.range = range;
            this.messenger = messenger;
        }

        public void Do(Vertex vertex)
        {
            if (vertex.IsObstacle)
            {
                vertex.IsObstacle = false;
            }
            else if (!range.IsInRange(vertex))
            {
                vertex.IsObstacle = true;
            }
            messenger.Send(new GraphChangedMessage());
        }
    }
}

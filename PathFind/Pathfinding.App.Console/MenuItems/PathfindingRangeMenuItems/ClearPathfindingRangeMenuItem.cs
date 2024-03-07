using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    [HighPriority]
    internal sealed class ClearPathfindingRangeMenuItem(IService storage,
        IPathfindingRangeBuilder<Vertex> rangeBuilder)
        : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IService storage = storage;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder = rangeBuilder;

        private int Id { get; set; }

        public bool CanBeExecuted()
        {
            return rangeBuilder.Range.HasSourceAndTargetSet() && Id != 0;
        }

        public void Execute()
        {
            storage.RemoveRange(Id);
            rangeBuilder.Undo();
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }

        public override string ToString()
        {
            return Languages.ClearPathfindingRange;
        }

        private void SetGraph(GraphMessage msg)
        {
            Id = msg.Graph.Id;
        }
    }
}

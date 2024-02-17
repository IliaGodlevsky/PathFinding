using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.Visualization.Extensions;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [LowPriority]
    internal sealed class ClearColorsMenuItem(IMessenger messenger,
        IPathfindingRangeBuilder<Vertex> rangeBuilder) 
        : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IMessenger messenger = messenger;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder = rangeBuilder;
        private GraphReadDto graph = GraphReadDto.Empty;

        public bool CanBeExecuted() => graph != GraphReadDto.Empty;

        public void Execute()
        {
            var msg = new StatisticsLineMessage(string.Empty);
            messenger.Send(msg, Tokens.AppLayout);
            graph.Graph.RestoreVerticesVisualState();
            rangeBuilder.Range.RestoreVerticesVisualState();
        }

        private void SetGraph(GraphMessage msg)
        {
            graph = msg.Graph;
        }

        public override string ToString()
        {
            return Languages.ClearColors;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }
    }
}

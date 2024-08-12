using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Read;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [LowPriority]
    internal sealed class ClearColorsMenuItem(IMessenger messenger,
        IPathfindingRangeBuilder<Vertex> rangeBuilder)
        : IConditionedMenuItem, ICanReceiveMessage
    {
        private readonly IMessenger messenger = messenger;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder = rangeBuilder;
        private GraphModel<Vertex> graph = null;

        public bool CanBeExecuted() => graph is not null;

        public async Task ExecuteAsync(CancellationToken token = default)
        {
            if (token.IsCancellationRequested) return;
            var msg = new StatisticsLineMessage(string.Empty);
            messenger.Send(msg, Tokens.AppLayout);
            graph.Graph.RestoreVerticesVisualState();
            rangeBuilder.Range.RestoreVerticesVisualState();
            await Task.CompletedTask;
        }

        private void SetGraph(GraphMessage msg)
        {
            graph = msg.Graph;
        }

        public override string ToString()
        {
            return Languages.ClearColors;
        }

        public void RegisterHandlers(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }
    }
}

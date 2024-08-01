using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    [HighPriority]
    internal sealed class ClearPathfindingRangeMenuItem(IRequestService<Vertex> service,
        IPathfindingRangeBuilder<Vertex> rangeBuilder)
        : IConditionedMenuItem, ICanReceiveMessage
    {
        private readonly IRequestService<Vertex> service = service;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder = rangeBuilder;

        private int Id { get; set; }

        public bool CanBeExecuted()
        {
            return rangeBuilder.Range.HasSourceAndTargetSet() && Id != 0;
        }

        public async Task ExecuteAsync(CancellationToken token = default)
        {
            if (token.IsCancellationRequested) return;
            await service.DeleteRangeAsync(Id, token);
            rangeBuilder.Undo();
        }

        public void RegisterHandlers(IMessenger messenger)
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

using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Requests.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems.Import
{
    [LowPriority]
    internal sealed class LoadPathfindingRangeMenuItem(ISerializer<IEnumerable<ICoordinate>> serializer,
        IPathfindingRangeBuilder<Vertex> rangeBuilder,
        IFilePathInput pathInput,
        ILog log,
        IRequestService<Vertex> service) : IConditionedMenuItem, ICanReceiveMessage
    {
        private readonly ISerializer<IEnumerable<ICoordinate>> serializer = serializer;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder = rangeBuilder;
        private readonly IInput<string> pathInput = pathInput;
        private readonly IRequestService<Vertex> service = service;
        private readonly ILog log = log;

        private GraphModel<Vertex> graph = null;

        public bool CanBeExecuted()
        {
            return graph is not null;
        }

        public async Task ExecuteAsync(CancellationToken token = default)
        {
            try
            {
                string path = pathInput.Input();
                var range = (await serializer.DeserializeFromFileAsync(path, token)).ToList();
                var vertices = range
                    .Select((x, i) => (Order: i, Id: graph.Graph.Get(x)))
                    .ToList();
                var createRangeRequest = new CreatePathfindingRangeRequest<Vertex>()
                {
                    Vertices = vertices,
                    GraphId = graph.Id
                };
                await service.CreateRangeAsync(createRangeRequest, token);
                rangeBuilder.Undo();
                rangeBuilder.Include(range, graph.Graph);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public void RegisterHandlers(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }

        private void SetGraph(GraphMessage msg)
        {
            graph = msg.Graph;
        }

        public override string ToString()
        {
            return Languages.LoadRange;
        }
    }
}

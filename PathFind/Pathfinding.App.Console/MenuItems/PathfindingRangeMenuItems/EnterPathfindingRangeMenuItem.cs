using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Service.Interface.Requests.Delete;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    [HighestPriority]
    internal sealed class EnterPathfindingRangeMenuItem(VertexActions actions,
        IInput<ConsoleKey> keyInput,
        IPathfindingRangeBuilder<Vertex> builder,
        IRequestService<Vertex> service) : NavigateThroughVerticesMenuItem(keyInput, service)
    {
        private readonly IPathfindingRangeBuilder<Vertex> builder = builder;
        private readonly VertexActions rangeActions = actions;

        public override bool CanBeExecuted()
        {
            return graph is not null && graph.Graph.GetNumberOfNotIsolatedVertices() > 1;
        }

        public async override Task ExecuteAsync(CancellationToken token = default)
        {
            if (token.IsCancellationRequested) return;
            await base.ExecuteAsync(token);
            processed.Clear();
            var currentRange = (await service.ReadRangeAsync(graph.Id, token))
                .Range
                .Select((x, i) => (Order: i, Coordinate: x))
                .ToDictionary(x => x.Coordinate, x => x.Order);
            var newRange = builder.Range.GetCoordinates()
                .Select((x, i) => (Order: i, Coordinate: x))
                .ToDictionary(x => x.Coordinate, x => x.Order);

            var added = new List<(int Order, Vertex Vertex)>();
            var updated = new List<(int Order, Vertex Vertex)>();

            foreach (var item in newRange)
            {
                var vertex = graph.Graph.Get(item.Key);
                var value = (item.Value, vertex);
                if (!currentRange.TryGetValue(item.Key, out var order))
                {
                    added.Add(value);
                }
                else if (item.Value != order)
                {
                    updated.Add(value);
                }
            }

            var deleted = currentRange.Where(x => !newRange.ContainsKey(x.Key))
                .Select(x => graph.Graph.Get(x.Key))
                .ToList();

            await service.DeleteRangeAsync(new DeleteRangeRequest<Vertex>()
            {
                Vertices = deleted,
                GraphId = graph.Id
            }, token);
            await service.UpdateRangeAsync(new()
            {
                Vertices = updated,
                GraphId = graph.Id
            }, token);
            await service.CreateRangeAsync(new()
            {
                Vertices = added,
                GraphId = graph.Id
            }, token);
        }

        public override string ToString()
        {
            return Languages.EnterPathfindingRange;
        }

        protected override VertexActions GetActions()
        {
            return rangeActions;
        }
    }
}

using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    [HighestPriority]
    internal sealed class EnterPathfindingRangeMenuItem(VertexActions actions,
        IInput<ConsoleKey> keyInput,
        IPathfindingRangeBuilder<Vertex> builder,
        IService service) : NavigateThroughVerticesMenuItem(keyInput, service)
    {
        private readonly IPathfindingRangeBuilder<Vertex> builder = builder;
        private readonly VertexActions rangeActions = actions;

        public override bool CanBeExecuted()
        {
            return graph.Graph.GetNumberOfNotIsolatedVertices() > 1;
        }

        public async override void Execute()
        {
            base.Execute();
            processed.Clear();
            var currentRange = service.GetRange(graph.Id)
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
                .ToReadOnly();

            await Task.Run(() =>
            {
                service.RemoveRange(deleted, graph.Id);
                service.UpdateRange(updated, graph.Id);
                service.AddRange(added, graph.Id);
            });
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

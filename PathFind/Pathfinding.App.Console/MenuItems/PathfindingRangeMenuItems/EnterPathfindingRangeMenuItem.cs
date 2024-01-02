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

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    [HighestPriority]
    internal sealed class EnterPathfindingRangeMenuItem : NavigateThroughVerticesMenuItem
    {
        private readonly IPathfindingRangeBuilder<Vertex> builder;
        private readonly VertexActions rangeActions;

        public EnterPathfindingRangeMenuItem(VertexActions actions,
            IInput<ConsoleKey> keyInput,
            IPathfindingRangeBuilder<Vertex> builder,
            IService service)
            : base(keyInput, service)
        {
            this.rangeActions = actions;
            this.builder = builder;
        }

        public override bool CanBeExecuted()
        {
            return graph.GetNumberOfNotIsolatedVertices() > 1;
        }

        public override void Execute()
        {
            base.Execute();

            var currentRange = service.GetRange(id)
                .Select((x, i) => (Order: i, Coordinate: x))
                .ToDictionary(x => x.Coordinate, x => x.Order);
            var newRange = builder.Range.GetCoordinates()
                .Select((x, i) => (Order: i, Coordinate: x))
                .ToDictionary(x => x.Coordinate, x => x.Order);

            var added = new List<(int Order, Vertex Vertex)>();
            var updated = new List<(int Order, Vertex Vertex)>();
            var deleted = new List<Vertex>();

            foreach (var item in newRange)
            {
                if (!currentRange.TryGetValue(item.Key, out var order))
                {
                    var vertex = graph.Get(item.Key);
                    added.Add((order, vertex));
                }

                if (currentRange.TryGetValue(item.Key, out order))
                {
                    if (item.Value != order)
                    {
                        var vertex = graph.Get(item.Key);
                        updated.Add((item.Value, vertex));
                    }
                }
            }

            foreach (var item in currentRange)
            {
                if (!newRange.TryGetValue(item.Key, out var order))
                {
                    var vertex = graph.Get(item.Key);
                    deleted.Add(vertex);
                }
            }

            service.AddRange(added.ToArray(), id);
            service.UpdateRange(updated.ToArray(), id);
            service.RemoveRange(deleted, id);
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

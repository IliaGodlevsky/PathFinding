using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using System;
using Shared.Extensions;
using Pathfinding.App.Console.DataAccess.Services;
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

            foreach (var item in newRange)
            {
                if (!currentRange.TryGetValue(item.Key, out var order))
                {
                    var vertex = graph.Get(item.Key);
                    service.AddRange(vertex, item.Value, id);
                }

                if (currentRange.TryGetValue(item.Key, out order))
                {
                    if (item.Value != order)
                    {
                        var vertex = graph.Get(item.Key);
                        service.UpdateRange(vertex, item.Value, id);
                    }
                }
            }

            foreach (var item in currentRange)
            {
                if (!newRange.TryGetValue(item.Key, out var order))
                {
                    var vertex = graph.Get(item.Key);
                    service.RemoveRange(vertex, id);
                }
            }
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

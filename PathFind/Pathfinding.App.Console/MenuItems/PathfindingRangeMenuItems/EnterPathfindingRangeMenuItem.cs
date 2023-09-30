using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Extensions;
using System;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    [HighestPriority]
    internal sealed class EnterPathfindingRangeMenuItem : SwitchVerticesMenuItem
    {
        private readonly GraphsPathfindingHistory history;
        private readonly IPathfindingRangeBuilder<Vertex> builder;

        public EnterPathfindingRangeMenuItem(VertexActions actions,
            IInput<ConsoleKey> keyInput, IPathfindingRangeBuilder<Vertex> builder,
            GraphsPathfindingHistory history)
            : base(actions, keyInput)
        {
            this.history = history;
            this.builder = builder;
        }

        public override bool CanBeExecuted()
        {
            return graph.GetNumberOfNotIsolatedVertices() > 1;
        }

        public override void Execute()
        {
            base.Execute();
            var hist = history.GetFor(graph);
            hist.PathfindingRange.Clear();
            var range = builder.Range.GetCoordinates();
            hist.PathfindingRange.AddRange(range);
        }

        public override string ToString()
        {
            return Languages.EnterPathfindingRange;
        }
    }
}

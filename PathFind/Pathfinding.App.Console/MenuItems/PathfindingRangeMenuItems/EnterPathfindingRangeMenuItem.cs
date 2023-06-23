using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    [HighestPriority]
    internal sealed class EnterPathfindingRangeMenuItem : SwitchVerticesMenuItem
    {
        private readonly PathfindingHistory history;
        private readonly IPathfindingRangeBuilder<Vertex> builder;

        public EnterPathfindingRangeMenuItem(IReadOnlyCollection<(string, IVertexAction)> actions,
            IInput<ConsoleKey> keyInput, IPathfindingRangeBuilder<Vertex> builder,
            PathfindingHistory history)
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
            var hist = history.History[graph];
            hist.PathfindingRange.Clear();
            hist.PathfindingRange.AddRange(builder.Range.GetCoordinates());
        }

        public override string ToString()
        {
            return Languages.EnterPathfindingRange;
        }
    }
}

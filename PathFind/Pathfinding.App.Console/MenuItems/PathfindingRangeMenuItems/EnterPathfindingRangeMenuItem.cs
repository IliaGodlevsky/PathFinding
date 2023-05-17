using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    [HighestPriority]
    internal sealed class EnterPathfindingRangeMenuItem : SwitchVerticesMenuItem
    {
        public EnterPathfindingRangeMenuItem(IReadOnlyDictionary<ConsoleKey, IVertexAction> actions,
            IInput<ConsoleKey> keyInput)
            : base(actions, keyInput)
        {

        }

        public override bool CanBeExecuted()
        {
            return graph.GetNumberOfNotIsolatedVertices() > 1;
        }

        public override string ToString()
        {
            return Languages.EnterPathfindingRange;
        }
    }
}

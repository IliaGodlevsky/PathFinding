using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    [HighestPriority]
    internal sealed class EnterPathfindingRangeMenuItem : SwitchVerticesMenuItem
    {
        protected override IReadOnlyDictionary<ConsoleKey, IVertexAction> Actions { get; }

        public EnterPathfindingRangeMenuItem(IMessenger messenger,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            IReadOnlyDictionary<ConsoleKey, IVertexAction> actions,
            IInput<ConsoleKey> keyInput)
            : base(messenger, keyInput)
        {
            Actions = actions.Append(new(ConsoleKey.Enter, new IncludeInRangeAction(rangeBuilder)))
                .Append(new(ConsoleKey.X, new ExcludeFromRangeAction(rangeBuilder)))
                .ToReadOnly();
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

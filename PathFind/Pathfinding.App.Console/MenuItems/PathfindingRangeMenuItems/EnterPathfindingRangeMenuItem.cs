using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
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
        private readonly IPathfindingRangeBuilder<Vertex> builder;

        public EnterPathfindingRangeMenuItem(IReadOnlyCollection<(string, IVertexAction)> actions,
            IPathfindingRangeBuilder<Vertex> builder,
            IMessenger messenger,
            IInput<ConsoleKey> keyInput)
            : base(actions, keyInput, messenger)
        {
            this.builder = builder;
        }

        public override bool CanBeExecuted()
        {
            return graph.GetNumberOfNotIsolatedVertices() > 1;
        }

        protected override void PostExecute()
        {
            messenger.SendData(builder.Range, Tokens.Storage);
        }

        public override string ToString()
        {
            return Languages.EnterPathfindingRange;
        }
    }
}

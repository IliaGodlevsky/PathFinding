using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.VertexActions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [MediumPriority]
    internal sealed class ReverseVertexMenuItem : SwitchVerticesMenuItem
    {
        protected override IReadOnlyDictionary<ConsoleKey, IVertexAction> Actions { get; }

        public ReverseVertexMenuItem(IMessenger messenger,
            IPathfindingRangeBuilder<Vertex> rangeBuilder,
            IInput<ConsoleKey> keyInput)
            : base(messenger, keyInput)
        {
            Actions = new Dictionary<ConsoleKey, IVertexAction>()
            {
                {ConsoleKey.Enter, new ReverseVertexAction(rangeBuilder.Range, messenger) }
            }.ToReadOnly();
        }

        public override string ToString()
        {
            return Languages.ReverseVertices;
        }
    }
}

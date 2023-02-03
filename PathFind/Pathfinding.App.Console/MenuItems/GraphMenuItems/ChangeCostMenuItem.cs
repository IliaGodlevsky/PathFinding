using GalaSoft.MvvmLight.Messaging;
using Org.BouncyCastle.Asn1;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model.VertexActions;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [MediumPriority]
    internal sealed class ChangeCostMenuItem : SwitchVerticesMenuItem
    {
        protected override IReadOnlyDictionary<ConsoleKey, IVertexAction> Actions { get; }

        public ChangeCostMenuItem(IMessenger messenger, IInput<ConsoleKey> keyInput)
            : base(messenger, keyInput)
        {
            Actions = new Dictionary<ConsoleKey, IVertexAction>()
            {
                { ConsoleKey.UpArrow, new IncreaseCostAction() },
                { ConsoleKey.DownArrow, new DecreaseCostAction() }
            }.ToReadOnly();
        }

        public override string ToString()
        {
            return Languages.ChangeCost;
        }
    }
}

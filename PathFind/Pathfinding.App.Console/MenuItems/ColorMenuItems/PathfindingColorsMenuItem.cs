using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.ColorMenuItems
{
    [MediumPriority]
    internal sealed class PathfindingColorsMenuItem : ColorsMenuItem
    {
        private ConsoleColor VisitedColor { get; set; }

        private ConsoleColor EnqueuedColor { get; set; }

        public PathfindingColorsMenuItem(IMessenger messenger, IInput<int> intInput)
            : base(messenger, intInput)
        {
        }

        protected override void SendColorsMessage()
        {
            messenger.SendData((Enqueued: EnqueuedColor, Visited: VisitedColor), Tokens.Pathfinding);
        }

        protected override IReadOnlyDictionary<string, string> GetColorsMap()
        {
            return new Dictionary<string, string>()
            {
                { nameof(EnqueuedColor), Languages.EnqueuedColor },
                { nameof(VisitedColor), Languages.VisitedColor }
            }.AsReadOnly();
        }

        public override string ToString()
        {
            return Languages.ChangePathfindingColors;
        }
    }
}

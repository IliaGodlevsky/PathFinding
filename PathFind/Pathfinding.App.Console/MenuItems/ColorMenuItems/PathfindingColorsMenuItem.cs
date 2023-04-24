using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using System;
using System.ComponentModel;

namespace Pathfinding.App.Console.MenuItems.ColorMenuItems
{
    [MediumPriority]
    internal sealed class PathfindingColorsMenuItem : ColorsMenuItem
    {
        [Description("Visited color")]
        private ConsoleColor VisitedColor { get; set; }

        [Description("Enqueued color")]
        private ConsoleColor EnqueuedColor { get; set; }

        public PathfindingColorsMenuItem(IMessenger messenger, IInput<int> intInput)
            : base(messenger, intInput)
        {
        }

        protected override void SendColorsMessage()
        {
            messenger.SendData((Enqueued: EnqueuedColor, Visited: VisitedColor), Tokens.Pathfinding);
        }

        public override string ToString()
        {
            return Languages.ChangePathfindingColors;
        }
    }
}

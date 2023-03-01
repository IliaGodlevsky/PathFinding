using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
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

        public override void RegisterHanlders(IMessenger messenger)
        {
            messenger.Register<PathfindingColorsMessage>(this, MessageTokens.PathfindingColorsChangeItem, RecieveColors);
        }

        protected override void SendAskMessage()
        {
            messenger.Send(new AskForPathfindingColorsMessage());
        }

        protected override void SendColorsMessage()
        {
            var message = new PathfindingColorsMessage(VisitedColor, EnqueuedColor);
            messenger.Send(message, MessageTokens.PathfindingColors);
        }

        private void RecieveColors(PathfindingColorsMessage msg)
        {
            VisitedColor = msg.VisitColor;
            EnqueuedColor = msg.EnqueuColor;
        }

        public override string ToString()
        {
            return Languages.ChangePathfindingColors;
        }
    }
}

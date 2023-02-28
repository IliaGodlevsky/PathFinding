using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
using System;
using System.ComponentModel;

namespace Pathfinding.App.Console.MenuItems.ColorMenuItems
{
    [MediumPriority]
    internal sealed class PathColorsMenuItem : ColorsMenuItem
    {
        [Description("Path color")]
        private ConsoleColor PathColor { get; set; }

        [Description("Crossed path color")]
        private ConsoleColor CrossedPathColor { get; set; }

        public PathColorsMenuItem(IMessenger messenger, IInput<int> intInput) 
            : base(messenger, intInput)
        {
        }

        public override void RegisterHanlders(IMessenger messenger)
        {
            messenger.Register<PathColorMessage>(this, MessageTokens.PathColorsChangeItem, ColorsRecieved);
        }

        protected override void SendAskMessage()
        {
            messenger.Send(new AskForPathColorsMessage());
        }

        protected override void SendColorsMessage()
        {
            var message = new PathColorMessage(PathColor, CrossedPathColor);
            messenger.Send(message, MessageTokens.PathColors);
        }

        private void ColorsRecieved(PathColorMessage msg)
        {
            PathColor = msg.PathColor;
            CrossedPathColor = msg.CrossedPathColor;
        }

        public override string ToString()
        {
            return "Change path colors";
        }
    }
}

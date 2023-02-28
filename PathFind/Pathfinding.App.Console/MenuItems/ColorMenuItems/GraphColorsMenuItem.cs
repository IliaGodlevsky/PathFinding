using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
using System;
using System.ComponentModel;

namespace Pathfinding.App.Console.MenuItems.ColorMenuItems
{
    [MediumPriority]
    internal sealed class GraphColorsMenuItem : ColorsMenuItem
    {
        [Description("Regular color")]
        private ConsoleColor RegularColor { get; set; }

        [Description("Obstacle color")]
        private ConsoleColor ObstacleColor { get; set; }

        public GraphColorsMenuItem(IMessenger messenger, IInput<int> intInput) 
            : base(messenger, intInput)
        {

        }

        public override void RegisterHanlders(IMessenger messenger)
        {
            messenger.Register<GraphColorsMessage>(this, MessageTokens.GraphColorsChangeItem, ColorsRecieved);
        }

        protected override void SendAskMessage()
        {
            messenger.Send(new AskForGraphColorsMessage());
        }

        protected override void SendColorsMessage()
        {
            var message = new GraphColorsMessage(RegularColor, ObstacleColor);
            messenger.Send(message, MessageTokens.GraphColors);
        }

        private void ColorsRecieved(GraphColorsMessage msg)
        {
            RegularColor = msg.RegularColor;
            ObstacleColor = msg.ObstacleColor;
        }

        public override string ToString()
        {
            return "Change graph colors";
        }
    }
}

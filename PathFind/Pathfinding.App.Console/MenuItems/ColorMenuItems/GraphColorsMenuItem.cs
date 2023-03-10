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

        protected override void SendColorsMessage()
        {
            messenger.SendData(new[] { RegularColor, ObstacleColor }, Tokens.Graph);
        }

        public override string ToString()
        {
            return Languages.ChangeGraphColors;
        }
    }
}

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

        protected override void SendColorsMessage()
        {
            messenger.SendData((Path:PathColor, Crossed: CrossedPathColor), Tokens.Path);
        }

        public override string ToString()
        {
            return Languages.ChangePathColors;
        }
    }
}

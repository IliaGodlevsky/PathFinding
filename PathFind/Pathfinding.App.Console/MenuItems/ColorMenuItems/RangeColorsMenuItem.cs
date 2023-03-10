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
    internal sealed class RangeColorsMenuItem : ColorsMenuItem
    {
        [Description("Source color")]
        private ConsoleColor SourceColor { get; set; }

        [Description("Target color")]
        private ConsoleColor TargetColor { get; set; }

        [Description("Transit color")]
        private ConsoleColor TransitColor { get; set; }

        public RangeColorsMenuItem(IMessenger messenger, IInput<int> intInput)
            : base(messenger, intInput)
        {

        }

        protected override void SendColorsMessage()
        {
            messenger.SendData(new[] { SourceColor, TargetColor, TransitColor }, Tokens.Range);
        }

        public override string ToString()
        {
            return Languages.ChangeRangeColors;
        }
    }
}
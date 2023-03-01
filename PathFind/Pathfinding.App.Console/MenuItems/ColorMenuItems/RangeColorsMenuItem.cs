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

        public override void RegisterHanlders(IMessenger messenger)
        {
            messenger.Register<RangeColorsMessage>(this, MessageTokens.RangeColorsChangeItem, RecieveColors);
        }

        protected override void SendAskMessage()
        {
            messenger.Send(new AskForRangeColorsMessage());
        }

        protected override void SendColorsMessage()
        {
            var message = new RangeColorsMessage(SourceColor, TargetColor, TransitColor);
            messenger.Send(message, MessageTokens.RangeColors);
        }

        private void RecieveColors(RangeColorsMessage msg)
        {
            SourceColor = msg.SourceColor;
            TargetColor = msg.TargetColor;
            TransitColor = msg.TransitColor;
        }

        public override string ToString()
        {
            return Languages.ChangeRangeColors;
        }
    }
}
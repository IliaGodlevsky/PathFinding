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
    internal sealed class RangeColorsMenuItem : ColorsMenuItem
    {
        private ConsoleColor SourceColor { get; set; }

        private ConsoleColor TargetColor { get; set; }

        private ConsoleColor TransitColor { get; set; }

        public RangeColorsMenuItem(IMessenger messenger, IInput<int> intInput)
            : base(messenger, intInput)
        {

        }

        protected override void SendColorsMessage()
        {
            messenger.SendData((Source: SourceColor, Target: TargetColor, Transit: TransitColor), Tokens.Range);
        }

        protected override IReadOnlyDictionary<string, string> GetColorsMap()
        {
            return new Dictionary<string, string>()
            {
                { nameof(SourceColor), Languages.SourceColor },
                { nameof(TargetColor), Languages.TargetColor },
                { nameof(TransitColor), Languages.TransitColor }
            }.AsReadOnly();
        }

        public override string ToString()
        {
            return Languages.ChangeRangeColors;
        }
    }
}
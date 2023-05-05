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
    internal sealed class PathColorsMenuItem : ColorsMenuItem
    {
        private ConsoleColor PathColor { get; set; }

        private ConsoleColor CrossedPathColor { get; set; }

        public PathColorsMenuItem(IMessenger messenger, IInput<int> intInput)
            : base(messenger, intInput)
        {
        }

        protected override void SendColorsMessage()
        {
            messenger.SendData((Path: PathColor, Crossed: CrossedPathColor), Tokens.Path);
        }

        protected override IReadOnlyDictionary<string, string> GetColorsMap()
        {
            return new Dictionary<string, string>()
            {
                { nameof(PathColor), Languages.PathColor },
                { nameof(CrossedPathColor), Languages.CrossedPathColor },
            }.AsReadOnly();
        }

        public override string ToString()
        {
            return Languages.ChangePathColors;
        }
    }
}

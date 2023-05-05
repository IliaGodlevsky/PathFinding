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
    internal sealed class GraphColorsMenuItem : ColorsMenuItem
    {
        private ConsoleColor RegularColor { get; set; }

        private ConsoleColor ObstacleColor { get; set; }

        public GraphColorsMenuItem(IMessenger messenger, IInput<int> intInput)
            : base(messenger, intInput)
        {

        }

        protected override void SendColorsMessage()
        {
            messenger.SendData((Regular: RegularColor, Obstacle: ObstacleColor), Tokens.Graph);
        }

        protected override IReadOnlyDictionary<string, string> GetColorsMap()
        {
            return new Dictionary<string, string>()
            {
                { nameof(RegularColor), Languages.RegularColor },
                { nameof(ObstacleColor), Languages.ObstacleColor },
            }.AsReadOnly();
        }

        public override string ToString()
        {
            return Languages.ChangeGraphColors;
        }
    }
}

using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;

namespace Pathfinding.App.Console.MenuItems.ColorMenuItems
{
    [LowPriority]
    internal sealed class ObstacleVertexColorMenuItem : ColorsMenuItem
    {
        protected override Tokens Token => Tokens.Obstacle;

        public ObstacleVertexColorMenuItem(IMessenger messenger, IInput<int> intInput) 
            : base(messenger, intInput)
        {
        }

        public override string ToString()
        {
            return Languages.ObstacleColor;
        }
    }
}

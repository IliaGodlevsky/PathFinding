using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;

namespace Pathfinding.App.Console.MenuItems.ColorMenuItems
{
    [HighPriority]
    internal sealed class TargetVertexColorMenuItem : ColorsMenuItem
    {
        protected override Tokens Token => Tokens.Target;

        public TargetVertexColorMenuItem(IMessenger messenger, IInput<int> intInput)
            : base(messenger, intInput)
        {
        }

        public override string ToString()
        {
            return Languages.TargetColor;
        }
    }
}

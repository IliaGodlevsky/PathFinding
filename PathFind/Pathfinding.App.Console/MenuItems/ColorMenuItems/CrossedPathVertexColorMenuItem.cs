using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;

namespace Pathfinding.App.Console.MenuItems.ColorMenuItems
{
    [LowPriority]
    internal sealed class CrossedPathVertexColorMenuItem : ColorsMenuItem
    {
        protected override IToken Token => Tokens.Crossed;

        public CrossedPathVertexColorMenuItem(IMessenger messenger, IInput<int> intInput)
            : base(messenger, intInput)
        {
        }

        public override string ToString()
        {
            return Languages.CrossedPathColor;
        }
    }
}

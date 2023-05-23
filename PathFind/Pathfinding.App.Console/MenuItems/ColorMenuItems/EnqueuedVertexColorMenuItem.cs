using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;

namespace Pathfinding.App.Console.MenuItems.ColorMenuItems
{
    [MediumPriority]
    internal sealed class EnqueuedVertexColorMenuItem : ColorsMenuItem
    {
        protected override IToken Token => Tokens.Enqueued;

        public EnqueuedVertexColorMenuItem(IMessenger messenger, IInput<int> intInput)
            : base(messenger, intInput)
        {
        }

        public override string ToString()
        {
            return Languages.EnqueuedColor;
        }
    }
}

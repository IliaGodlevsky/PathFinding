using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Settings;

namespace Pathfinding.App.Console.MenuItems.ColorMenuItems
{
    [LowPriority]
    internal sealed class CrossedPathVertexColorMenuItem : ColorsMenuItem
    {
        protected override string SettingKey { get; } = nameof(Colours.CrossedPathColor);

        public CrossedPathVertexColorMenuItem(IInput<int> intInput)
            : base(intInput)
        {
        }
    }
}

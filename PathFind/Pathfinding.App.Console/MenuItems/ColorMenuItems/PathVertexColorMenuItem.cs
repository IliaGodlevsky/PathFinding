using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Settings;

namespace Pathfinding.App.Console.MenuItems.ColorMenuItems
{
    [MediumPriority]
    internal sealed class PathVertexColorMenuItem : ColorsMenuItem
    {
        protected override string SettingKey { get; } = nameof(Colours.PathColor);

        public PathVertexColorMenuItem(IInput<int> intInput)
            : base(intInput)
        {
        }
    }
}

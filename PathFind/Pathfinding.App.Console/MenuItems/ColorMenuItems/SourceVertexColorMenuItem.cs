using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Settings;

namespace Pathfinding.App.Console.MenuItems.ColorMenuItems
{
    [HighPriority]
    internal sealed class SourceVertexColorMenuItem : ColorsMenuItem
    {
        protected override string SettingKey { get; } = nameof(Colours.SourceColor);

        public SourceVertexColorMenuItem(IInput<int> intInput)
            : base(intInput)
        {
        }

        public override string ToString()
        {
            return Languages.SourceColor;
        }
    }
}

using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Units;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.MainMenuItems
{
    [LowPriority]
    internal sealed class ColorsUnitMenuItem : UnitDisplayMenuItem<ColorsUnit>
    {
        public ColorsUnitMenuItem(IViewFactory viewFactory, ColorsUnit unit, ILog log) 
            : base(viewFactory, unit, log)
        {
        }

        public override string ToString()
        {
            return Languages.ColorsUnitMenuItem;
        }
    }
}

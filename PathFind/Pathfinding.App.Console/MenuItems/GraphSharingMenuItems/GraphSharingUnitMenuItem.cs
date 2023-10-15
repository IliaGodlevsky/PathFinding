using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Units;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.GraphSharingMenuItems
{
    [LowPriority]
    internal sealed class GraphSharingUnitMenuItem : UnitDisplayMenuItem<GraphSharingUnit>
    {
        public GraphSharingUnitMenuItem(IInput<int> intInput, GraphSharingUnit unit, ILog log)
            : base(intInput, unit, log)
        {
        }

        public override string ToString()
        {
            return Languages.GraphSharing;
        }
    }
}

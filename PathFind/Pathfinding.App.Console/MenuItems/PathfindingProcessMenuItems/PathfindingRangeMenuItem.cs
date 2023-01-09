using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Units;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [HighPriority]
    internal class PathfindingRangeMenuItem : UnitDisplayMenuItem<PathfindingRangeUnit>
    {
        public PathfindingRangeMenuItem(IViewFactory viewFactory, PathfindingRangeUnit viewModel, ILog log)
            : base(viewFactory, viewModel, log)
        {

        }

        public override string ToString()
        {
            return Languages.PathfindingRange;
        }
    }
}

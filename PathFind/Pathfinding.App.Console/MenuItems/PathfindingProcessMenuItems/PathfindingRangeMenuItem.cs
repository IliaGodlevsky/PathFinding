using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Units;
using Pathfinding.Logging.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [Order(2)]
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

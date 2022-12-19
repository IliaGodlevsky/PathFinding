using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Units;
using Pathfinding.Logging.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [Order(3)]
    internal sealed class HistoryMenuItem : UnitDisplayMenuItem<PathfindingHistoryUnit>
    {
        public HistoryMenuItem(IViewFactory viewFactory, PathfindingHistoryUnit viewModel, ILog log) 
            : base(viewFactory, viewModel, log)
        {
        }

        public override string ToString()
        {
            return Languages.History;
        }
    }
}

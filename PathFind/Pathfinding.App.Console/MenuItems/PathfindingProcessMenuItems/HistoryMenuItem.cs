using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Units;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    internal sealed class HistoryMenuItem : UnitDisplayMenuItem<PathfindingHistoryUnit>
    {
        public override int Order => 3;

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

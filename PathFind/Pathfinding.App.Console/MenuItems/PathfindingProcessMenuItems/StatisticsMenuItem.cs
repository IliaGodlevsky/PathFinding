using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Units;
using Pathfinding.Logging.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [Order(4)]
    internal sealed class StatisticsMenuItem : UnitDisplayMenuItem<PathfindingStatisticsUnit>
    {
        public StatisticsMenuItem(IViewFactory viewFactory, PathfindingStatisticsUnit viewModel, ILog log)
            : base(viewFactory, viewModel, log)
        {

        }

        public override string ToString()
        {
            return Languages.Statistics;
        }
    }
}
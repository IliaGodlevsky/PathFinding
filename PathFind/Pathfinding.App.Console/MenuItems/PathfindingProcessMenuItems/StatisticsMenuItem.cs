using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.ViewModel;
using Pathfinding.Logging.Interface;
using System;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    internal sealed class StatisticsMenuItem : UnitDisplayMenuItem<PathfindingStatisticsUnit>
    {
        public override int Order => 4;

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
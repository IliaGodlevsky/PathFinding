﻿using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Units;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [MediumPriority]
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
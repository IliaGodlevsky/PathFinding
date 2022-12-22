﻿using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Units;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems
{
    internal sealed class MainUnitMenuItem : UnitDisplayMenuItem<MainUnit>
    {
        public MainUnitMenuItem(IViewFactory viewFactory, MainUnit viewModel, ILog log) 
            : base(viewFactory, viewModel, log)
        {

        }
    }
}
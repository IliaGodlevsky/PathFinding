using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Settings;
using Pathfinding.App.Console.Units;
using Pathfinding.Logging.Interface;
using System;

namespace Pathfinding.App.Console.MenuItems
{
    internal sealed class MainUnitMenuItem : UnitDisplayMenuItem<MainUnit>, IDisposable
    {
        public MainUnitMenuItem(IViewFactory viewFactory, MainUnit viewModel, ILog log)
            : base(viewFactory, viewModel, log)
        {

        }

        public void Dispose()
        {
            Colours.Default.Save();
            Keys.Default.Save();
        }
    }
}

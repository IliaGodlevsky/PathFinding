using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Units;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems
{
    internal sealed class MainUnitMenuItem(IInput<int> input,
        MainUnit viewModel, ILog log)
        : UnitDisplayMenuItem<MainUnit>(input, viewModel, log)
    {
    }
}

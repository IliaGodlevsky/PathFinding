using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Units;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.MainMenuItems
{
    [HighestPriority]
    internal sealed class GraphCreateMenuItem : UnitDisplayMenuItem<GraphUnit>
    {
        public GraphCreateMenuItem(IInput<int> input, GraphUnit viewModel, ILog log) 
            : base(input, viewModel, log)
        {

        }

        public override string ToString()
        {
            return Languages.Graph;
        }
    }
}

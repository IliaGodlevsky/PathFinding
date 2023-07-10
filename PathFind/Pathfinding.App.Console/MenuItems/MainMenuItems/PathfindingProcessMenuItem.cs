using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Units;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.MainMenuItems
{
    [HighPriority]
    internal sealed class PathfindingProcessMenuItem : MainMenuItem<PathfindingProcessUnit>
    {
        public PathfindingProcessMenuItem(IInput<int> input, PathfindingProcessUnit viewModel, 
            IMessenger messenger, ILog log) 
            : base(input, viewModel, messenger, log)
        {

        }

        public override string ToString()
        {
            return Languages.Pathfinding;
        }
    }
}

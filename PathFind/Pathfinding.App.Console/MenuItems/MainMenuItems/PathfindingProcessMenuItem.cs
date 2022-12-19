using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Units;
using Pathfinding.Logging.Interface;
using Shared.Primitives.Attributes;

namespace Pathfinding.App.Console.MenuItems.MainMenuItems
{
    [Order(2)]
    internal sealed class PathfindingProcessMenuItem : MainMenuItem<PathfindingProcessUnit>
    {
        public PathfindingProcessMenuItem(IViewFactory viewFactory, PathfindingProcessUnit viewModel, 
            IMessenger messenger, ILog log) 
            : base(viewFactory, viewModel, messenger, log)
        {

        }

        public override string ToString()
        {
            return Languages.Pathfinding;
        }
    }
}

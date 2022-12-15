using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.ViewModel;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.MainMenuItems
{
    internal sealed class PathfindingProcessMenuItem : MainMenuItem<PathfindingProcessUnit>
    {
        public override int Order => 2;

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

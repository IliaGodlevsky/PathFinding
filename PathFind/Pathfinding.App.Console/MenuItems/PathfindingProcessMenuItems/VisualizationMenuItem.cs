using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.ViewModel;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    internal sealed class VisualizationMenuItem : UnitDisplayMenuItem<PathfindingVisualizationUnit>
    {
        public override int Order => 2;

        public VisualizationMenuItem(IViewFactory viewFactory, 
            PathfindingVisualizationUnit viewModel, ILog log) : base(viewFactory, viewModel, log)
        {

        }

        public override string ToString()
        {
            return Languages.Visual;
        }
    }
}

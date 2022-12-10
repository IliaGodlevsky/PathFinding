using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.ViewModel;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.MenuItems.MainMenuItems
{
    internal sealed class GraphCreateMenuItem : UnitDisplayMenuItem<GraphCreatingUnit>
    {
        public override int Order => 1;

        public GraphCreateMenuItem(IViewFactory viewFactory, 
            GraphCreatingUnit viewModel, ILog log) 
            : base(viewFactory, viewModel, log)
        {
        }

        public override string ToString()
        {
            return "Graph";
        }
    }
}

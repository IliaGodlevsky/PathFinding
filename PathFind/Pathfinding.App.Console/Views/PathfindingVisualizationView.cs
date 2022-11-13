using Pathfinding.App.Console.ViewModel;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.Views
{
    internal sealed class PathfindingVisualizationView : View
    {
        public PathfindingVisualizationView(PathfindingVisualizationViewModel model, ILog log) 
            : base(model, log)
        {
        }
    }
}

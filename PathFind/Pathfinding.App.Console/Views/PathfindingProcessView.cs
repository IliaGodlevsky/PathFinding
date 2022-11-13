using Pathfinding.App.Console.ViewModel;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.Views
{
    internal sealed class PathfindingProcessView : View
    {
        public PathfindingProcessView(PathfindingProcessViewModel model, ILog log) 
            : base(model, log)
        {
        }
    }
}

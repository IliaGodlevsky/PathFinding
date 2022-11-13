using Pathfinding.App.Console.ViewModel;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.Views
{
    internal sealed class PathfindingView : View
    {
        public PathfindingView(PathfindingViewModel model, ILog log) 
            : base(model, log)
        {
            
        }
    }
}

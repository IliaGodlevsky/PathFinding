using Pathfinding.App.Console.ViewModel;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.Views
{
    internal sealed class PathfindingHistoryView : View
    {
        public PathfindingHistoryView(PathfindingHistoryViewModel model, ILog log) 
            : base(model, log)
        {
        }
    }
}

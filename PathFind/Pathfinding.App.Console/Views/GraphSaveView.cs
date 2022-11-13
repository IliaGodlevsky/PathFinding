using Pathfinding.App.Console.ViewModel;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.Views
{
    internal sealed class GraphSaveView : View
    {
        public GraphSaveView(GraphSaveViewModel model, ILog log) : base(model, log)
        {
        }
    }
}

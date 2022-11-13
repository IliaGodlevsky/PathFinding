using Pathfinding.App.Console.ViewModel;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.Views
{
    internal sealed class VertexStateView : View
    {
        public VertexStateView(VertexStateViewModel model, ILog log) 
            : base(model, log)
        {
        }
    }
}

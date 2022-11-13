using Pathfinding.App.Console.ViewModel;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.Views
{
    internal sealed class MainView : View
    {
        public MainView(MainViewModel model, ILog log) 
            : base(model, log)
        {
            
        }
    }
}
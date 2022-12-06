using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Views;
using Pathfinding.Logging.Interface;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ViewFactory : IViewFactory
    {
        private readonly ILog log;
        private readonly IInput<int> input;

        public ViewFactory(ILog log, IInput<int> input)
        {
            this.log = log;
            this.input = input;
        }

        public View CreateView(IViewModel viewModel)
        {
            return new View(viewModel, log) { IntInput = input };
        }
    }
}

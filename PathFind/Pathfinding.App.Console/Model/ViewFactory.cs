using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Views;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ViewFactory : IViewFactory
    {
        private readonly IInput<int> input;

        public ViewFactory(IInput<int> input)
        {
            this.input = input;
        }

        public View CreateView(IUnit viewModel)
        {
            return new View(viewModel, input);
        }
    }
}

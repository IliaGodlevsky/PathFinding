using Pathfinding.ConsoleApp.ViewModel;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class HeuristicsViewModelChangedMessage
    {
        public IRequireHeuristicsViewModel ViewModel { get; }

        public HeuristicsViewModelChangedMessage(IRequireHeuristicsViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}

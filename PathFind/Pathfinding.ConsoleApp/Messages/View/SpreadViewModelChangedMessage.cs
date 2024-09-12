using Pathfinding.ConsoleApp.ViewModel;

namespace Pathfinding.ConsoleApp.Messages.View
{
    internal sealed class SpreadViewModelChangedMessage
    {
        public IRequireSpreadViewModel ViewModel { get; }

        public SpreadViewModelChangedMessage(IRequireSpreadViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}

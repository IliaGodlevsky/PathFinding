using Pathfinding.ConsoleApp.ViewModel;

namespace Pathfinding.ConsoleApp.Messages.View
{
    internal sealed class RunViewModelChangedMessage
    {
        public CreateRunButtonViewModel RunViewModel { get; }

        public RunViewModelChangedMessage(CreateRunButtonViewModel runViewModel)
        {
            RunViewModel = runViewModel;
        }
    }
}

using Pathfinding.ConsoleApp.ViewModel;

namespace Pathfinding.ConsoleApp.Messages.View
{
    internal sealed class RunViewModelChangedMessage
    {
        public CreateRunViewModel RunViewModel { get; }

        public RunViewModelChangedMessage(CreateRunViewModel runViewModel)
        {
            RunViewModel = runViewModel;
        }
    }
}

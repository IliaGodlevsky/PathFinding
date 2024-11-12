using Pathfinding.ConsoleApp.ViewModel.Interface;

namespace Pathfinding.ConsoleApp.Messages.View
{
    internal sealed record class HeuristicsViewModelChangedMessage(IRequireHeuristicsViewModel ViewModel);
}

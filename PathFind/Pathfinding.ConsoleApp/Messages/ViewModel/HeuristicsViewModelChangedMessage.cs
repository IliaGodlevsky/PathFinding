using Pathfinding.ConsoleApp.ViewModel;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed record class HeuristicsViewModelChangedMessage(IRequireHeuristicsViewModel ViewModel);
}

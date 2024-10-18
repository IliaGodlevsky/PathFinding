using Pathfinding.ConsoleApp.ViewModel;

namespace Pathfinding.ConsoleApp.Messages.View
{
    internal sealed record class HeuristicsViewModelChangedMessage(IRequireHeuristicsViewModel ViewModel);
}

using Pathfinding.ConsoleApp.ViewModel;
using Pathfinding.ConsoleApp.ViewModel.Interface;

namespace Pathfinding.ConsoleApp.Messages.View
{
    internal sealed record class PathfindingViewModelChangedMessage(IPathfindingProcessViewModel ViewModel);
}

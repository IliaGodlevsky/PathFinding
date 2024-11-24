using ReactiveUI;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IPathfindingProcessViewModel
    {
        ReactiveCommand<Unit, Unit> StartAlgorithmCommand { get; }
    }
}
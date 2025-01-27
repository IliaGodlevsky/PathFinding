using ReactiveUI;
using System.Reactive;

namespace Pathfinding.App.Console.ViewModel.Interface
{
    internal interface IPathfindingProcessViewModel
    {
        ReactiveCommand<Unit, Unit> StartAlgorithmCommand { get; }
    }
}
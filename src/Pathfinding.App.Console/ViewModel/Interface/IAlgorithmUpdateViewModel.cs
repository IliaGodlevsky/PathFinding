using ReactiveUI;
using System.Reactive;

namespace Pathfinding.App.Console.ViewModel.Interface
{
    internal interface IAlgorithmUpdateViewModel
    {
        ReactiveCommand<Unit, Unit> UpdateAlgorithmsCommand { get; }
    }
}
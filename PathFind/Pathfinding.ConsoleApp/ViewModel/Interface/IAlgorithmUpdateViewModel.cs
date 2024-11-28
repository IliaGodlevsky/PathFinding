using ReactiveUI;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IAlgorithmUpdateViewModel
    {
        ReactiveCommand<Unit, Unit> UpdateAlgorithmsCommand { get; }
    }
}
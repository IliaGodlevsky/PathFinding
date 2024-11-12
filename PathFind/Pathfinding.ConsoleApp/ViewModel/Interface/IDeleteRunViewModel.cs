using ReactiveUI;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IDeleteRunViewModel
    {
        ReactiveCommand<Unit, Unit> DeleteRunCommand { get; }
    }
}
using ReactiveUI;
using System.Reactive;

namespace Pathfinding.App.Console.ViewModel.Interface
{
    internal interface IDeleteRunViewModel
    {
        ReactiveCommand<Unit, Unit> DeleteRunCommand { get; }
    }
}
using ReactiveUI;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IGraphDeleteViewModel
    {
        ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    }
}
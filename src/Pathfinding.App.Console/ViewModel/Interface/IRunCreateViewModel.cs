using ReactiveUI;
using System.Reactive;

namespace Pathfinding.App.Console.ViewModel.Interface
{
    internal interface IRunCreateViewModel
    {
        ReactiveCommand<Unit, Unit> CreateRunCommand { get; }
    }
}
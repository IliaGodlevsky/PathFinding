using ReactiveUI;
using System.Reactive;

namespace Pathfinding.App.Console.ViewModel.Interface
{
    internal interface IRunUpdateViewModel
    {
        ReactiveCommand<Unit, Unit> UpdateRunsCommand { get; }
    }
}
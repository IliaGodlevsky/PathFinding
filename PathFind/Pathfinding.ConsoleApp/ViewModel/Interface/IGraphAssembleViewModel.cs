using ReactiveUI;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal interface IGraphAssembleViewModel
    {
        ReactiveCommand<Unit, Unit> CreateCommand { get; }
    }
}
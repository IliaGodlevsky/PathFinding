using ReactiveUI;
using System.Reactive;

namespace Pathfinding.App.Console.ViewModel.Interface
{
    internal interface IGraphCopyViewModel
    {
        ReactiveCommand<Unit, Unit> CopyGraphCommand { get; }
    }
}
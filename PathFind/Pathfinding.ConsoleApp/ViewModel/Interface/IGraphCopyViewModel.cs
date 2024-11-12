using ReactiveUI;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IGraphCopyViewModel
    {
        ReactiveCommand<Unit, Unit> CopyGraphCommand { get; }
    }
}
using Pathfinding.App.Console.Model;
using ReactiveUI;
using System.Reactive;

namespace Pathfinding.App.Console.ViewModel.Interface
{
    internal interface IRunRangeViewModel
    {
        ReactiveCommand<GraphVertexModel, Unit> AddToRangeCommand { get; }

        ReactiveCommand<GraphVertexModel, Unit> RemoveFromRangeCommand { get; }

        ReactiveCommand<Unit, Unit> DeletePathfindingRange { get; }
    }
}
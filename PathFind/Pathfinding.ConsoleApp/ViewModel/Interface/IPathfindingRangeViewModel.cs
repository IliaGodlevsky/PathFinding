using Pathfinding.ConsoleApp.Model;
using ReactiveUI;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IPathfindingRangeViewModel
    {
        ReactiveCommand<GraphVertexModel, Unit> AddToRangeCommand { get; }

        ReactiveCommand<Unit, Unit> DeletePathfindingRange { get; }

        ReactiveCommand<GraphVertexModel, Unit> RemoveFromRangeCommand { get; }
    }
}
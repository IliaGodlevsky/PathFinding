using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Interface;
using ReactiveUI;
using System.Reactive;

namespace Pathfinding.ConsoleApp.ViewModel.Interface
{
    internal interface IGraphFieldViewModel
    {
        IGraph<GraphVertexModel> Graph { get; }

        ReactiveCommand<GraphVertexModel, Unit> DecreaseVertexCostCommand { get; }

        ReactiveCommand<GraphVertexModel, Unit> IncreaseVertexCostCommand { get; }

        ReactiveCommand<GraphVertexModel, Unit> ReverseVertexCommand { get; }
    }
}
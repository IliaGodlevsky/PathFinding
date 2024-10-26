using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Undefined;
using ReactiveUI;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class AStarLeeAlgorithmViewModel : PathfindingProcessViewModel,
        IRequireHeuristicsViewModel
    {
        private (string Name, IHeuristic Heuristic) heuristic;
        public (string Name, IHeuristic Heuristic) Heuristic
        {
            get => heuristic;
            set => this.RaiseAndSetIfChanged(ref heuristic, value);
        }

        private double weight;
        public double Weight 
        {
            get => weight;
            set => this.RaiseAndSetIfChanged(ref weight, value);
        }

        public AStarLeeAlgorithmViewModel(
            IRequestService<GraphVertexModel> service,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ILog logger)
            : base(service, messenger, logger)
        {
        }

        public override string AlgorithmId { get; } = AlgorithmNames.AStarLee;

        protected override void AppendStatistics(RunStatisticsModel model)
        {
            model.Heuristics = heuristic.Name;
            model.Weight = Weight;
        }

        protected override PathfindingProcess GetAlgorithm(IEnumerable<GraphVertexModel> pathfindingRange)
        {
            return new AStarLeeAlgorithm(pathfindingRange,
                heuristic.Heuristic.ToWeighted(Weight));
        }
    }
}

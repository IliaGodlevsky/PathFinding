using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Undefined;
using ReactiveUI;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class CreateDepthAlgorithmRunViewModel : CreateRunButtonViewModel, 
        IRequireHeuristicsViewModel
    {
        private (string Name, IHeuristic Heuristic) heuristic;
        public (string Name, IHeuristic Heuristic) Heuristic 
        {
            get => heuristic;
            set => this.RaiseAndSetIfChanged(ref  heuristic, value);
        }

        public CreateDepthAlgorithmRunViewModel(IRequestService<VertexModel> service, 
            [KeyFilter(KeyFilters.ViewModels)]IMessenger messenger,
            ILog logger) : base(service, messenger, logger)
        {
        }

        protected override string AlgorithmId { get; } = "Depth";

        protected override void AppendStatistics(RunStatisticsModel model)
        {
            model.Heuristics = heuristic.Name;
        }

        protected override PathfindingProcess GetAlgorithm(IEnumerable<VertexModel> pathfindingRange)
        {
            return new DepthFirstAlgorithm(pathfindingRange, heuristic.Heuristic);
        }
    }
}

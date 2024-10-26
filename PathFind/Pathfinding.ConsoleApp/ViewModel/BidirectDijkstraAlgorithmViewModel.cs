using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Undefined;
using ReactiveUI;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class BidirectDijkstraAlgorithmViewModel 
        : PathfindingProcessViewModel, IRequireStepRuleViewModel
    {
        private (string Name, IStepRule Rule) stepRule;
        public (string Name, IStepRule Rule) StepRule 
        {
            get => stepRule;
            set => this.RaiseAndSetIfChanged(ref stepRule, value);
        }

        public override string AlgorithmId { get; } = AlgorithmNames.BidirectDijkstra;

        public BidirectDijkstraAlgorithmViewModel(
            IRequestService<GraphVertexModel> service, 
            [KeyFilter(KeyFilters.ViewModels)]IMessenger messenger, ILog logger)
            : base(service, messenger, logger)
        {
        }

        protected override void AppendStatistics(RunStatisticsModel model)
        {
            model.StepRule = stepRule.Name;
        }

        protected override PathfindingProcess GetAlgorithm(IEnumerable<GraphVertexModel> pathfindingRange)
        {
            return new BidirectDijkstraAlgorithm(pathfindingRange, stepRule.Rule);
        }
    }
}

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
    internal sealed class CreateHeuristicsCostRunViewModel : CreateRunButtonViewModel,
        IRequireHeuristicsViewModel, IRequireStepRuleViewModel
    {
        private (string Name, IHeuristic Heuristic) heuristic;
        public (string Name, IHeuristic Heuristic) Heuristic 
        {
            get => heuristic; 
            set => this.RaiseAndSetIfChanged(ref heuristic, value);
        }

        private (string Name, IStepRule StepRule) stepRule;
        public (string Name, IStepRule Rule) StepRule 
        {
            get => stepRule;
            set => this.RaiseAndSetIfChanged(ref stepRule, value);
        }

        public CreateHeuristicsCostRunViewModel(IRequestService<VertexModel> service,
            [KeyFilter(KeyFilters.ViewModels)]IMessenger messenger, ILog logger) 
            : base(service, messenger, logger)
        {
        }

        protected override string AlgorithmId { get; } = "Distance+";

        protected override void AppendStatistics(RunStatisticsModel model)
        {
            model.StepRule = stepRule.Name;
            model.Heuristics = heuristic.Name;
        }

        protected override PathfindingProcess GetAlgorithm(IEnumerable<VertexModel> pathfindingRange)
        {
            return new HeuristicCostGreedyAlgorithm(pathfindingRange, 
                heuristic.Heuristic, stepRule.StepRule);
        }
    }
}

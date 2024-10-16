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
    internal sealed class CreateIDAStarRunViewModel : CreateRunButtonViewModel,
        IRequireStepRuleViewModel, IRequireHeuristicsViewModel, IRequireSpreadViewModel
    {
        private (string Name, IStepRule Rule) stepRule;
        public (string Name, IStepRule Rule) StepRule
        {
            get => stepRule;
            set => this.RaiseAndSetIfChanged(ref stepRule, value);
        }

        private (string Name, IHeuristic Heuristic) heuristic;
        public (string Name, IHeuristic Heuristic) Heuristic
        {
            get => heuristic;
            set => this.RaiseAndSetIfChanged(ref heuristic, value);
        }

        private (string Name, int Spread) spreadLevel;
        public (string Name, int Spread) SpreadLevel
        {
            get => spreadLevel;
            set => this.RaiseAndSetIfChanged(ref spreadLevel, value);
        }

        public override string AlgorithmId => AlgorithmNames.IDAStar;

        public CreateIDAStarRunViewModel(IRequestService<VertexModel> service,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ILog logger) : base(service, messenger, logger)
        {
        }

        protected override void AppendStatistics(RunStatisticsModel model)
        {
            model.Heuristics = Heuristic.Name;
            model.StepRule = StepRule.Name;
            model.Spread = SpreadLevel.Name;
        }

        protected override PathfindingProcess GetAlgorithm(IEnumerable<VertexModel> pathfindingRange)
        {
            return new IDAStarAlgorithm(pathfindingRange, stepRule.Rule,
                heuristic.Heuristic, spreadLevel.Spread);
        }
    }
}

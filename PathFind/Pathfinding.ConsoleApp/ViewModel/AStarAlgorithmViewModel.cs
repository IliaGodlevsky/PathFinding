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
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class AStarAlgorithmViewModel : PathfindingProcessViewModel,
        IRequireStepRuleViewModel, IRequireHeuristicsViewModel
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

        private double weight;
        public double Weight 
        {
            get => weight;
            set => this.RaiseAndSetIfChanged(ref weight, value);
        }

        public AStarAlgorithmViewModel(IRequestService<GraphVertexModel> service,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ILog logger) : base(service, messenger, logger)
        {
        }

        public override string AlgorithmId => AlgorithmNames.AStar;

        protected override void AppendStatistics(CreateStatisticsRequest model)
        {
            model.StepRule = stepRule.Name;
            model.Heuristics = heuristic.Name;
            model.Weight = Weight;
        }

        protected override PathfindingProcess GetAlgorithm(IEnumerable<GraphVertexModel> pathfindingRange)
        {
            return new AStarAlgorithm(pathfindingRange,
                stepRule.Rule, heuristic.Heuristic.WithWeight(Weight));
        }
    }
}

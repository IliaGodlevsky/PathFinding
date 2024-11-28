using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms.Exceptions;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Business.Builders;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Requests.Create;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class PathfindingProcessViewModel : BaseViewModel, 
        IPathfindingProcessViewModel,
        IRequireHeuristicsViewModel,
        IRequireStepRuleViewModel,
        IRequireAlgorithmNameViewModel
    {
        private sealed record AlgorithmBuildInfo(HeuristicFunctions? Heuristics,
            double? Weight, StepRules? StepRule) : IAlgorithmBuildInfo;

        private readonly IRequestService<GraphVertexModel> service;
        private readonly IMessenger messenger;
        private readonly ILog logger;

        public ReactiveCommand<Unit, Unit> StartAlgorithmCommand { get; }

        private Algorithms? algorithm;
        public Algorithms? Algorithm
        {
            get => algorithm;
            set => this.RaiseAndSetIfChanged(ref algorithm, value);
        }

        private HeuristicFunctions? heuristic;
        public HeuristicFunctions? Heuristic
        {
            get => heuristic;
            set => this.RaiseAndSetIfChanged(ref heuristic, value);
        }

        private double? weight;
        public double? Weight 
        {
            get => weight;
            set => this.RaiseAndSetIfChanged(ref weight, value);
        }

        private StepRules? stepRule;
        public StepRules? StepRule
        { 
            get => stepRule; 
            set => this.RaiseAndSetIfChanged(ref stepRule, value);
        }

        private GraphModel<GraphVertexModel> graph = GraphModel<GraphVertexModel>.Empty;
        private GraphModel<GraphVertexModel> Graph
        {
            get => graph;
            set => this.RaiseAndSetIfChanged(ref graph, value);
        }

        public PathfindingProcessViewModel(IRequestService<GraphVertexModel> service,
            [KeyFilter(KeyFilters.ViewModels)]IMessenger messenger,
            ILog logger)
        {
            this.messenger = messenger;
            this.service = service;
            this.logger = logger;
            StartAlgorithmCommand = ReactiveCommand.CreateFromTask(StartAlgorithm,
                CanStartAlgorithm());
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
        }

        private IObservable<bool> CanStartAlgorithm()
        {
            return this.WhenAnyValue(
                x => x.Graph.Graph,
                x => x.Graph.Id,
                x => x.Heuristic,
                x => x.Weight,
                x => x.Algorithm,
                (graph, id, heuristic, weight, algorithm) =>
                {
                    bool canExecute = id > 0 && graph != null 
                        && algorithm != null
                        && Enum.IsDefined(typeof(Algorithms), algorithm.Value);
                    if (heuristic != default)
                    {
                        return canExecute && weight > 0;
                    }
                    return canExecute;
                });
        }

        private void OnGraphActivated(object recipient, GraphActivatedMessage msg)
        {
            Graph = msg.Graph;
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            if (msg.GraphIds.Contains(Graph.Id))
            {
                Graph = new();
            }
        }

        private async Task StartAlgorithm()
        {
            var pathfindingRange = (await service.ReadRangeAsync(Graph.Id)
                .ConfigureAwait(false))
                .Select(x => Graph.Graph.Get(x.Position))
                .ToList();

            if (pathfindingRange.Count > 1)
            {
                int visitedCount = 0;
                void OnVertexProcessed(object sender, EventArgs e) => visitedCount++;
                var status = RunStatuses.Success;
                var buildInfo = new AlgorithmBuildInfo(Heuristic, Weight, StepRule);
                var algorithm = AlgorithmBuilder.TakeAlgorithm(Algorithm.Value)
                    .WithAlgorithmInfo(buildInfo)
                    .Build(pathfindingRange);
                algorithm.VertexProcessed += OnVertexProcessed;
                var path = NullGraphPath.Interface;
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    path = algorithm.FindPath();
                }
                catch (PathfindingException ex)
                {
                    status = RunStatuses.Failure;
                    logger.Warn(ex);
                }
                catch (Exception ex)
                {
                    status = RunStatuses.Failure;
                    logger.Error(ex);
                }

                stopwatch.Stop();
                algorithm.VertexProcessed -= OnVertexProcessed;

                var request = new CreateStatisticsRequest()
                {
                    Algorithm = Algorithm.Value,
                    Cost = path.Cost,
                    Steps = path.Count,
                    StepRule = StepRule,
                    Heuristics = Heuristic,
                    Weight = Weight,
                    Visited = visitedCount,
                    Elapsed = stopwatch.Elapsed,
                    ResultStatus = status,
                    GraphId = Graph.Id
                };

                await ExecuteSafe(async () =>
                {
                    var result = await service.CreateStatisticsAsync(request)
                        .ConfigureAwait(false);
                    messenger.Send(new RunCreatedMessaged(result));
                }, logger.Error).ConfigureAwait(false);
            }
            else
            {
                logger.Info("Pathfinding range is not set");
            }
        }
    }
}

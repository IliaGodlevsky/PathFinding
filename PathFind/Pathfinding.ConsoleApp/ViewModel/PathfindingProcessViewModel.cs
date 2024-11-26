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
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
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
        private readonly IRequestService<GraphVertexModel> service;
        private readonly IMessenger messenger;
        private readonly ILog logger;

        private AlgorithmBuilder algorithmBuilder = null;
        private StatisticsBuilder statisitcsBuilder = null;

        public ReactiveCommand<Unit, Unit> StartAlgorithmCommand { get; }

        private Algorithms algorithm;
        public Algorithms Algorithm
        {
            get => algorithm;
            set
            {
                this.RaiseAndSetIfChanged(ref algorithm, value);
                algorithmBuilder = AlgorithmBuilder.TakeAlgorithm(Algorithm);
                statisitcsBuilder?.WithAlgorithm(Algorithm);
            }
        }

        private HeuristicFunctions? heuristic;
        public HeuristicFunctions? Heuristic
        {
            get => heuristic;
            set
            {
                this.RaiseAndSetIfChanged(ref heuristic, value);
                statisitcsBuilder?.WithHeuristics(Heuristic);
                if (Heuristic != null && algorithmBuilder != null)
                {
                    algorithmBuilder.WithHeuristics(Heuristic.Value);
                }
            }
        }

        private double? weight;
        public double? Weight 
        {
            get => weight;
            set
            {
                this.RaiseAndSetIfChanged(ref weight, value);
                statisitcsBuilder?.WithWeight(Weight);
                if (Weight != null && algorithmBuilder != null)
                {
                    algorithmBuilder.WithWeight(Weight.Value);
                }
            }
        }

        private StepRules? stepRule;
        public StepRules? StepRule
        { 
            get => stepRule; 
            set 
            {
                this.RaiseAndSetIfChanged(ref stepRule, value);
                statisitcsBuilder?.WithStepRules(StepRule);
                if (StepRule != null && algorithmBuilder != null)
                {
                    algorithmBuilder.WithStepRules(StepRule.Value);
                }
            }
        }

        private GraphModel<GraphVertexModel> graph = GraphModel<GraphVertexModel>.Empty;
        private GraphModel<GraphVertexModel> Graph
        {
            get => graph;
            set
            {
                this.RaiseAndSetIfChanged(ref graph, value);
                statisitcsBuilder = StatisticsBuilder.TakeGraph(Graph.Id);
            }
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
                        && Enum.IsDefined(typeof(Algorithms), algorithm);
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

            if (pathfindingRange.Count > 1 
                && algorithmBuilder != null 
                && statisitcsBuilder != null)
            {
                int visitedCount = 0;

                void OnVertexProcessed(object sender, EventArgs e) => visitedCount++;

                var status = RunStatuses.Success;

                var algorithm = algorithmBuilder.Build(pathfindingRange);

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
                finally
                {
                    stopwatch.Stop();
                    algorithm.VertexProcessed -= OnVertexProcessed;
                }

                var request = statisitcsBuilder.WithElapsed(stopwatch.Elapsed)
                        .WithStatus(status).WithPath(path)
                        .WithVisited(visitedCount).Build();

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

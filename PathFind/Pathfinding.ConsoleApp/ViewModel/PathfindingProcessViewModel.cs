using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Infrastructure.Business.Algorithms.Exceptions;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Requests.Create;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System;
using System.Collections.Generic;
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

        public ReactiveCommand<Unit, Unit> StartAlgorithmCommand { get; }

        private string algorithmName;
        public string AlgorithmName
        {
            get => algorithmName;
            set => this.RaiseAndSetIfChanged(ref algorithmName, value);
        }

        private string heuristic;
        public string Heuristic
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

        private string stepRule;
        public string StepRule
        { 
            get => stepRule; 
            set => this.RaiseAndSetIfChanged(ref  stepRule, value);
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
                x => x.AlgorithmName,
                (graph, id, heuristic, weight, algorithm) =>
                {
                    bool canExecute = id > 0 && graph != null && !string.IsNullOrEmpty(algorithm);
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

        private PathfindingProcess GetAlgorithm(string algorithmName, 
            IEnumerable<GraphVertexModel> pathfindingRange)
        {
            return algorithmName switch
            {
                AlgorithmNames.AStar => new AStarAlgorithm(pathfindingRange, GetStatistics(StepRule), GetHeuristics(Heuristic)),
                AlgorithmNames.AStarGreedy => new AStarGreedyAlgorithm(pathfindingRange, GetHeuristics(Heuristic), GetStatistics(StepRule)),
                AlgorithmNames.AStarLee => new AStarLeeAlgorithm(pathfindingRange, GetHeuristics(Heuristic)),
                AlgorithmNames.BidirectAStar => new BidirectAStarAlgorithm(pathfindingRange, GetStatistics(StepRule), GetHeuristics(Heuristic)),
                AlgorithmNames.BidirectDijkstra => new BidirectDijkstraAlgorithm(pathfindingRange, GetStatistics(StepRule)),
                AlgorithmNames.BidirectLee => new BidirectLeeAlgorithm(pathfindingRange),
                AlgorithmNames.CostGreedy => new CostGreedyAlgorithm(pathfindingRange, GetStatistics(StepRule)),
                AlgorithmNames.DepthFirst => new DepthFirstAlgorithm(pathfindingRange),
                AlgorithmNames.Dijkstra => new DijkstraAlgorithm(pathfindingRange, GetStatistics(StepRule)),
                AlgorithmNames.DistanceFirst => new DistanceFirstAlgorithm(pathfindingRange, GetHeuristics(Heuristic)),
                AlgorithmNames.Lee => new LeeAlgorithm(pathfindingRange),
                AlgorithmNames.Snake => new SnakeAlgorithm(pathfindingRange, GetHeuristics(Heuristic)),
                _ => throw new NotImplementedException($"{algorithmName} is not implemented"),
            };
        }

        private IHeuristic GetHeuristics(string heuristicName)
        {
            return heuristicName switch
            {
                HeuristicNames.Euclidian => new EuclidianDistance().WithWeight(Weight),
                HeuristicNames.Chebyshev => new ChebyshevDistance().WithWeight(Weight),
                HeuristicNames.Diagonal => new DiagonalDistance().WithWeight(Weight),
                HeuristicNames.Manhattan => new ManhattanDistance().WithWeight(Weight),
                HeuristicNames.Cosine => new CosineDistance().WithWeight(Weight),
                _ => throw new NotImplementedException($"Unknown heuristic: {heuristicName}")
            };
        }

        private IStepRule GetStatistics(string stepRule)
        {
            return stepRule switch
            {
                StepRuleNames.Default => new DefaultStepRule(),
                StepRuleNames.Landscape => new LandscapeStepRule(),
                _ => throw new NotImplementedException($"Unknown step rule: {stepRule}")
            };
        }


        private void AppendStatistics(CreateStatisticsRequest request) 
        {
            request.StepRule = StepRule;
            request.Heuristics = Heuristic;
            if (Heuristic != default)
            {
                request.Weight = Weight;
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
                var subAlgorithms = new List<SubAlgorithmModel>();
                int visitedCount = 0;
                var visitedVertices = new List<(Coordinate Visited, IReadOnlyList<Coordinate> Enqueued)>();

                void AddSubAlgorithm(IReadOnlyCollection<Coordinate> path = null)
                {
                    subAlgorithms.Add(new()
                    {
                        Order = subAlgorithms.Count,
                        Visited = visitedVertices.ToArray(),
                        Path = path ?? Array.Empty<Coordinate>()
                    });
                    visitedCount += visitedVertices.Count;
                    visitedVertices.Clear();
                }
                void OnVertexProcessed(object sender, VerticesProcessedEventArgs e)
                {
                    visitedVertices.Add((e.Current, e.Enqueued));
                }
                void OnSubPathFound(object sender, SubPathFoundEventArgs args)
                {
                    AddSubAlgorithm(args.SubPath);
                }

                string status = RunStatuses.Success;

                var algorithm = GetAlgorithm(AlgorithmName, pathfindingRange);

                algorithm.SubPathFound += OnSubPathFound;
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
                    AddSubAlgorithm();
                }
                catch (Exception ex)
                {
                    status = RunStatuses.Failure;
                    logger.Error(ex);
                    AddSubAlgorithm();
                }
                finally
                {
                    stopwatch.Stop();
                    algorithm.SubPathFound -= OnSubPathFound;
                    algorithm.VertexProcessed -= OnVertexProcessed;
                }

                var request = ModelBuilder.CreateStatisticsRequest()
                    .WithStatistics(Graph.Id, AlgorithmName, path,
                        visitedCount, status, stopwatch.Elapsed);

                AppendStatistics(request);

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

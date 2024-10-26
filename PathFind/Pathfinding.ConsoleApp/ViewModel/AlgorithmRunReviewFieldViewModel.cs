using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Infrastructure.Data.Pathfinding.Factories;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Undefined;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class AlgorithmRunReviewFieldViewModel : AlgorithmRunBaseViewModel
    {
        private readonly IReadOnlyDictionary<string, IHeuristic> heuristics;
        private readonly IReadOnlyDictionary<string, IStepRule> stepRules;
        private readonly ILog log;

        public AlgorithmRunReviewFieldViewModel(
            IGraphAssemble<RunVertexModel> graphAssemble,
            StepRulesViewModel stepRulesViewModel,
            HeuristicsViewModel heuristicViewModel,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ILog log)
            : base(graphAssemble)
        {
            messenger.Register<RunActivatedMessage>(this, async (r, msg) => await OnRunActivated(r, msg));
            this.heuristics = heuristicViewModel.Heuristics;
            this.stepRules = stepRulesViewModel.StepRules;
            this.log = log;
        }

        private async Task OnRunActivated(object recipient, RunActivatedMessage msg)
        {
            var subAlgorithms = new List<SubAlgorithmModel>();
            var visitedVertices = new List<(Coordinate Visited, IReadOnlyList<Coordinate> Enqueued)>();

            void AddSubAlgorithm(IReadOnlyCollection<Coordinate> path = null)
            {
                subAlgorithms.Add(new()
                {
                    Order = subAlgorithms.Count,
                    Visited = visitedVertices.ToArray(),
                    Path = path ?? Array.Empty<Coordinate>()
                });
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

            var graph = await CreateGraph(msg.Run);
            var range = msg.Run.GraphState.Range.Select(graph.Get).ToArray();
            var algorithm = GetAlgorithm(msg.Run.Statistics, range);

            algorithm.SubPathFound += OnSubPathFound;
            algorithm.VertexProcessed += OnVertexProcessed;

            try
            {
                algorithm.FindPath();
            }
            catch (NotImplementedException ex)
            {
                log.Warn(ex, ex.Message);
                AddSubAlgorithm();
            }
            catch (Exception)
            {
                AddSubAlgorithm();
            }
            finally
            {
                algorithm.SubPathFound -= OnSubPathFound;
                algorithm.VertexProcessed -= OnVertexProcessed;
            }
            Vertices = GetVerticesStates(subAlgorithms, msg.Run.GraphState.Range.ToList(), graph);
            GraphState = graph;
        }

        private PathfindingProcess GetAlgorithm(RunStatisticsModel statistics, IReadOnlyCollection<RunVertexModel> range)
        {
            var stepRule = stepRules.GetOrDefault(statistics.StepRule ?? string.Empty);
            var heuristic = heuristics.GetOrDefault(statistics.Heuristics ?? string.Empty);
            double? weight = statistics.Weight;

            switch (statistics.AlgorithmId)
            {
                case AlgorithmNames.Dijkstra: 
                    return new DijkstraAlgorithm(range, stepRule);
                case AlgorithmNames.BidirectDijkstra: 
                    return new BidirectDijkstraAlgorithm(range, stepRule);
                case AlgorithmNames.DepthFirst: 
                    return new DepthFirstAlgorithm(range);
                case AlgorithmNames.AStar: 
                    return new AStarAlgorithm(range, stepRule, heuristic.ToWeighted(weight));
                case AlgorithmNames.BidirectAStar: 
                    return new BidirectAStarAlgorithm(range, stepRule, heuristic.ToWeighted(weight));
                case AlgorithmNames.CostGreedy: 
                    return new CostGreedyAlgorithm(range, stepRule);
                case AlgorithmNames.DistanceFirst: 
                    return new DistanceFirstAlgorithm(range, heuristic.ToWeighted(weight));
                case AlgorithmNames.Snake: 
                    return new SnakeAlgorithm(range, heuristic.ToWeighted(weight));
                case AlgorithmNames.AStarGreedy: 
                    return new AStarGreedyAlgorithm(range, heuristic.ToWeighted(weight), stepRule);
                case AlgorithmNames.DepthRandom: 
                    return new DepthRandomAlgorithm(range);
                case AlgorithmNames.Lee: 
                    return new LeeAlgorithm(range);
                case AlgorithmNames.BidirectLee: 
                    return new BidirectLeeAlgorithm(range);
                case AlgorithmNames.AStarLee: 
                    return new AStarLeeAlgorithm(range, heuristic.ToWeighted(weight));
                case AlgorithmNames.Random:
                    return new RandomAlgorithm(range);
                default: 
                    throw new NotImplementedException($"Unknown algorithm name: {statistics.AlgorithmId}");
            }
        }

        protected override async Task<IGraph<RunVertexModel>> CreateGraph(AlgorithmRunHistoryModel model)
        {
            var graph = await base.CreateGraph(model);
            var factory = model.GraphInfo.Neighborhood == NeighborhoodNames.Moore
                ? (INeighborhoodFactory)new MooreNeighborhoodFactory()
                : new VonNeumannNeighborhoodFactory();
            var layer = new NeighborhoodLayer(factory);
            layer.Overlay((IGraph<IVertex>)graph);
            return graph;
        }
    }
}

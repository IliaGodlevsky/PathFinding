using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System.Collections.Generic;
using System;
using Pathfinding.Service.Interface.Models.Read;
using System.Linq;
using Pathfinding.Domain.Core;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Shared.Extensions;
using Pathfinding.Infrastructure.Business.Extensions;
using System.Threading.Tasks;
using Pathfinding.Service.Interface.Extensions;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class AlgorithmRunFieldViewModel : BaseViewModel
    {
        private readonly ILog log;
        private readonly IMessenger messenger;
        private readonly Dictionary<int, List<VertexState>> cache = new();

        private IReadOnlyDictionary<Coordinate, RunVertexModel> runGraph;
        public IReadOnlyDictionary<Coordinate, RunVertexModel> RunGraph
        {
            get
            {
                if (runGraph == null && Graph != null 
                    && Graph != Graph<GraphVertexModel>.Empty)
                {
                    runGraph = Graph.ToRunVertices();
                    this.RaisePropertyChanged();
                }
                return runGraph;
            }
            private set => this.RaiseAndSetIfChanged(ref runGraph, value);
        }

        private int GraphId { get; set; }

        private IGraph<GraphVertexModel> Graph { get; set; }

        private int Cursor { get; set; } = 0;

        private List<VertexState> VerticesStates { get; set; } = new();

        public ReactiveCommand<int, bool> ProcessNextCommand { get; }

        public ReactiveCommand<int, bool> ReverseNextCommand { get; }

        public AlgorithmRunFieldViewModel(
            [KeyFilter(KeyFilters.ViewModels)]IMessenger messenger,
            ILog log)
        {
            this.messenger = messenger;
            this.log = log;
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
            messenger.Register<RunSelectedMessage>(this, async (r, msg) => await OnRunActivated(r, msg));
            messenger.Register<GraphBecameReadOnlyMessage>(this, OnGraphBecameReadOnly);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
            ProcessNextCommand = ReactiveCommand.Create<int, bool>(ProcessNext);
            ReverseNextCommand = ReactiveCommand.Create<int, bool>(ReverseNext);
        }

        private void OnGraphBecameReadOnly(object recipient, GraphBecameReadOnlyMessage msg)
        {
            if (!msg.Became)
            {
                Clear();
            }
        }

        private void Clear()
        {
            RunGraph = new Dictionary<Coordinate, RunVertexModel>();
            cache.Clear();
            runGraph = null;
            Cursor = 0;
            VerticesStates.Clear();
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            if (msg.GraphIds.Contains(GraphId))
            {
                Graph = Graph<GraphVertexModel>.Empty;
                GraphId = 0;
                Clear();
            }
        }

        private void OnGraphActivated(object recipient, GraphActivatedMessage msg)
        {
            Graph = msg.Graph.Graph;
            GraphId = msg.Graph.Id;
            Clear();
        }

        private bool ProcessNext(int number)
        {
            while (number-- > 0 && Cursor < VerticesStates.Count)
            {
                var state = VerticesStates.ElementAtOrDefault(Cursor);
                if (state.Vertex != default)
                {
                    state.SetState(); 
                }
                Cursor++;
            }
            return Cursor < VerticesStates.Count;
        }

        private bool ReverseNext(int number)
        {
            while (number-- > 0 && Cursor > 0)
            {
                Cursor--;
                var state = VerticesStates.ElementAtOrDefault(Cursor);
                if (state.Vertex != default)
                {
                    state.ToReverted().SetState();
                }
            }
            return Cursor > 0;
        }

        private void ProcessToCursor()
        {
            int cursor = 0;
            while (cursor < Cursor)
            {
                var state = VerticesStates.ElementAtOrDefault(cursor);
                if (state.Vertex != default)
                {
                    state.SetState();
                }
                cursor++;
            }
        }

        private async Task OnRunActivated(object recipient, RunSelectedMessage msg)
        {
            var run = msg.SelectedRuns.First();
            if (cache.TryGetValue(run.RunId, out var vertices))
            {
                RunGraph.Values.ClearState();
                VerticesStates = vertices;
                ProcessToCursor();
                return;
            }
            var rangeMsg = new QueryPathfindingRangeMessage();
            messenger.Send(rangeMsg);
            var rangeCoordinates = rangeMsg.PathfindingRange;
            var range = rangeCoordinates.Select(x => RunGraph[x]).ToArray();

            var subAlgorithms = new List<SubAlgorithmModel>();
            var visitedVertices = new List<(Coordinate Visited, IReadOnlyList<Coordinate> Enqueued)>();

            void AddSubAlgorithm(IReadOnlyCollection<Coordinate> path = null)
            {
                subAlgorithms.Add(new()
                {
                    Order = subAlgorithms.Count,
                    Visited = visitedVertices,
                    Path = path ?? Array.Empty<Coordinate>()
                });
                visitedVertices = new();
            }
            void OnVertexProcessed(object sender, VerticesProcessedEventArgs e)
            {
                visitedVertices.Add((e.Current, e.Enqueued));
            }
            void OnSubPathFound(object sender, SubPathFoundEventArgs args)
            {
                AddSubAlgorithm(args.SubPath);
            }

            try
            {
                var algorithm = run.GetAlgorithm(range);
                algorithm.SubPathFound += OnSubPathFound;
                algorithm.VertexProcessed += OnVertexProcessed;
                try
                {
                    await algorithm.FindPathAsync();
                }
                finally
                {
                    algorithm.SubPathFound -= OnSubPathFound;
                    algorithm.VertexProcessed -= OnVertexProcessed;
                }
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
            RunGraph.Values.ClearState();
            VerticesStates = await Task.Run(() => RunGraph.ToVerticesStates(subAlgorithms, rangeCoordinates));
            cache[run.RunId] = VerticesStates;
            ProcessToCursor();
        }
    }

    enum RunState { Source, Target, Transit, Visited, Enqueued, Path, CrossPath }

    readonly struct VertexState
    {
        public RunVertexModel Vertex { get; }

        public RunState State { get; }

        public bool Value { get; }

        public VertexState(RunVertexModel vertex, RunState state, bool value)
        {
            Vertex = vertex;
            State = state;
            Value = value;
        }

        public VertexState ToReverted() => new(Vertex, State, !Value);
    }

    file static class Extensions
    {
        public static PathfindingProcess GetAlgorithm(this RunInfoModel statistics, IReadOnlyCollection<RunVertexModel> range)
        {
            return statistics.Name switch
            {
                AlgorithmNames.Dijkstra => new DijkstraAlgorithm(range, GetStepRule(statistics)),
                AlgorithmNames.BidirectDijkstra => new BidirectDijkstraAlgorithm(range, GetStepRule(statistics)),
                AlgorithmNames.DepthFirst => new DepthFirstAlgorithm(range),
                AlgorithmNames.AStar => new AStarAlgorithm(range, GetStepRule(statistics), GetHeuristic(statistics)),
                AlgorithmNames.BidirectAStar => new BidirectAStarAlgorithm(range, GetStepRule(statistics), GetHeuristic(statistics)),
                AlgorithmNames.CostGreedy => new CostGreedyAlgorithm(range, GetStepRule(statistics)),
                AlgorithmNames.DistanceFirst => new DistanceFirstAlgorithm(range, GetHeuristic(statistics)),
                AlgorithmNames.Snake => new SnakeAlgorithm(range),
                AlgorithmNames.AStarGreedy => new AStarGreedyAlgorithm(range, GetHeuristic(statistics), GetStepRule(statistics)),
                AlgorithmNames.Lee => new LeeAlgorithm(range),
                AlgorithmNames.BidirectLee => new BidirectLeeAlgorithm(range),
                AlgorithmNames.AStarLee => new AStarLeeAlgorithm(range, GetHeuristic(statistics)),
                _ => throw new NotImplementedException($"Unknown algorithm name: {statistics.Name}")
            };
        }

        public static void ClearState(this IEnumerable<RunVertexModel> vertices)
        {
            foreach (var vertex in vertices)
            {
                vertex.IsTarget = false;
                vertex.IsCrossedPath = false;
                vertex.IsTransit = false;
                vertex.IsVisited = false;
                vertex.IsPath = false;
                vertex.IsEnqueued = false;
                vertex.IsSource = false;
            }
        }

        public static Dictionary<Coordinate, RunVertexModel> ToRunVertices(this IGraph<GraphVertexModel> graph)
        {
            var result = new Dictionary<Coordinate, RunVertexModel>();
            foreach (var vertex in graph)
            {
                var runVertex = new RunVertexModel(vertex.Position)
                {
                    Cost = new VertexCost(vertex.Cost.CurrentCost, vertex.Cost.CostRange),
                    IsObstacle = vertex.IsObstacle
                };
                result.Add(runVertex.Position, runVertex);
            }
            foreach (var vertex in graph)
            {
                var runVertex = result[vertex.Position];
                foreach (var neighbor in vertex.Neighbours)
                {
                    var runVertexNeighbor = result[neighbor.Position];
                    runVertex.Neighbours.Add(runVertexNeighbor);
                }
            }
            return result;
        }

        public static List<VertexState> ToVerticesStates(
            this IReadOnlyDictionary<Coordinate, RunVertexModel> RunGraph,
            IEnumerable<SubAlgorithmModel> subAlgorithms,
            IReadOnlyCollection<Coordinate> range)
        {
            var vertices = new List<VertexState>();

            vertices.Add(new (RunGraph[range.First()], RunState.Source, true));
            foreach (var transit in range.Skip(1).Take(range.Count - 2))
            {
                vertices.Add(new (RunGraph[transit], RunState.Transit, true));
            }
            vertices.Add(new (RunGraph[range.Last()], RunState.Target, true));

            var previousVisited = new HashSet<Coordinate>();
            var previousPaths = new HashSet<Coordinate>();
            var previousEnqueued = new HashSet<Coordinate>();

            foreach (var subAlgorithm in subAlgorithms)
            {
                var visitedIgnore = range.Concat(previousPaths).ToArray();
                foreach (var (Visited, Enqueued) in subAlgorithm.Visited)
                {
                    foreach (var enqueued in Enqueued.Intersect(previousVisited).Except(visitedIgnore))
                    {
                        vertices.Add(new (RunGraph[enqueued], RunState.Visited, false));
                    }
                    foreach (var visited in Visited.Enumerate().Except(visitedIgnore))
                    {
                        vertices.Add(new (RunGraph[visited], RunState.Visited, true));
                    }
                    foreach (var enqueued in Enqueued.Except(visitedIgnore).Except(previousEnqueued))
                    {
                        vertices.Add(new (RunGraph[enqueued], RunState.Enqueued, true));
                    }
                }
                var exceptRangePath = subAlgorithm.Path.Except(range).ToArray();
                foreach (var path in exceptRangePath.Intersect(previousPaths))
                {
                    vertices.Add(new (RunGraph[path], RunState.CrossPath, true));
                }
                foreach (var path in exceptRangePath.Except(previousPaths))
                {
                    vertices.Add(new (RunGraph[path], RunState.Path, true));
                }

                previousVisited.AddRange(subAlgorithm.Visited.Select(x => x.Visited));
                previousEnqueued.AddRange(subAlgorithm.Visited.SelectMany(x => x.Enqueued));
                previousPaths.AddRange(subAlgorithm.Path);
            }
            return vertices;
        }

        public static void SetState(this VertexState state)
        {
            switch (state.State)
            {
                case RunState.Visited: state.Vertex.IsVisited = state.Value; break;
                case RunState.Enqueued: state.Vertex.IsEnqueued = state.Value; break;
                case RunState.CrossPath: state.Vertex.IsCrossedPath = state.Value; break;
                case RunState.Path: state.Vertex.IsPath = state.Value; break;
                case RunState.Source: state.Vertex.IsSource = state.Value; break;
                case RunState.Target: state.Vertex.IsTarget = state.Value; break;
                case RunState.Transit: state.Vertex.IsTransit = state.Value; break;
            }
        }

        private static IHeuristic GetHeuristic(this RunInfoModel statistics)
        {
            var weight = double.Parse(statistics.Weight);
            return statistics.Heuristics switch
            {
                HeuristicNames.Euclidian => new EuclidianDistance().WithWeight(weight),
                HeuristicNames.Chebyshev => new ChebyshevDistance().WithWeight(weight),
                HeuristicNames.Diagonal => new DiagonalDistance().WithWeight(weight),
                HeuristicNames.Manhattan => new ManhattanDistance().WithWeight(weight),
                HeuristicNames.Cosine => new CosineDistance().WithWeight(weight),
                _ => throw new NotImplementedException($"Unknown heuristic: {statistics.Heuristics}")
            };
        }

        private static IStepRule GetStepRule(this RunInfoModel statistics)
        {
            return statistics.StepRule switch
            {
                StepRuleNames.Default => new DefaultStepRule(),
                StepRuleNames.Landscape => new LandscapeStepRule(),
                _ => throw new NotImplementedException($"Unknown step rule: {statistics.StepRule}")
            };
        }
    }
}

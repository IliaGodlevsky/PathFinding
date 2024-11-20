using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Infrastructure.Business.Algorithms.Heuristics;
using Pathfinding.Infrastructure.Business.Algorithms.StepRules;
using Pathfinding.Infrastructure.Business.Extensions;
using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class AlgorithmRunFieldViewModel : BaseViewModel, IAlgorithmRunFieldViewModel
    {
        private readonly ILog log;
        private readonly IMessenger messenger;
        private readonly IGraphAssemble<RunVertexModel> graphAssemble;
        private readonly Dictionary<int, List<VertexState>> cache = new();

        private readonly CompositeDisposable disposables = new();

        private int graphId;
        private List<VertexState> verticesStates = new();

        private int Cursor { get; set; } = 0;

        public IGraph<RunVertexModel> RunGraph { get; private set; } = Graph<RunVertexModel>.Empty;

        public ReactiveCommand<float, Unit> ProcessNextCommand { get; }

        public ReactiveCommand<float, Unit> ReverseNextCommand { get; }

        public ReactiveCommand<float, Unit> ProcessToCommand { get; }

        private float fraction;
        public float Fraction 
        {
            get => fraction;
            set => this.RaiseAndSetIfChanged(ref fraction, value);
        }

        public AlgorithmRunFieldViewModel(
            IGraphAssemble<RunVertexModel> graphAssemble,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger,
            ILog log)
        {
            this.messenger = messenger;
            this.log = log;
            this.graphAssemble = graphAssemble;
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
            messenger.Register<RunSelectedMessage>(this, OnRunActivated);
            messenger.Register<GraphBecameReadOnlyMessage>(this, OnGraphBecameReadOnly);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
            ProcessNextCommand = ReactiveCommand.Create<float>(ProcessNext);
            ReverseNextCommand = ReactiveCommand.Create<float>(ReverseNext);
            ProcessToCommand = ReactiveCommand.Create<float>(ProcessTo);
        }

        private void OnGraphBecameReadOnly(object recipient, GraphBecameReadOnlyMessage msg)
        {
            if (!msg.Became && graphId == msg.Id)
            {
                Clear();
            }
        }

        private void Clear()
        {
            cache.Clear();
            Cursor = 0;
            Fraction = 0;
            verticesStates.Clear();
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            if (msg.GraphIds.Contains(graphId))
            {
                graphId = 0;
                RunGraph = Graph<RunVertexModel>.Empty;
                this.RaisePropertyChanged(nameof(RunGraph));
                disposables.Clear();
                Clear();
            }
        }

        private void OnGraphActivated(object recipient, GraphActivatedMessage msg)
        {
            graphId = msg.Graph.Id;
            var graphVertices = msg.Graph.Graph;
            var layer = new GraphLayer(graphVertices);
            var graph = graphAssemble.AssembleGraph(layer,
                graphVertices.DimensionsSizes);
            RunGraph = graph;
            disposables.Clear();
            foreach (var vertex in graphVertices)
            {
                var runVertex = graph.Get(vertex.Position);
                vertex.WhenAnyValue(x => x.IsObstacle)
                    .BindTo(runVertex, x => x.IsObstacle)
                    .DisposeWith(disposables);
                vertex.WhenAnyValue(x => x.Cost)
                    .Do(x => runVertex.Cost = x.DeepClone())
                    .Subscribe()
                    .DisposeWith(disposables);
            }
            Clear();
        }

        private void ProcessNext(float fraction)
        {
            ProcessForward((int)Math.Round(verticesStates.Count * fraction, 0));
            if (Cursor > verticesStates.Count) Cursor = verticesStates.Count;
        }

        private void ProcessForward(int count)
        {
            while (count-- > 0 && Cursor < verticesStates.Count)
            {
                verticesStates.ElementAtOrDefault(Cursor++).SetState();
            }
        }

        private void ProcessBackward(int count)
        {
            while (count-- > 0 && Cursor > 0)
            {
                verticesStates.ElementAtOrDefault(--Cursor).ToReverted().SetState();
            }
        }

        private void ProcessTo(float fraction)
        {
            int count = (int)Math.Round(verticesStates.Count * fraction, 0);
            if (count > Cursor)
            {
                ProcessForward(count - Cursor);
            }
            else
            {
                ProcessBackward(Cursor - count);
            }
        }

        private void ReverseNext(float fraction)
        {
            ProcessBackward((int)Math.Round(verticesStates.Count * fraction, 0));
            if (Cursor < 0) Cursor = 0;
        }

        private void ProcessToFraction()
        {
            Cursor = 0;
            ProcessForward((int)Math.Round(verticesStates.Count * Fraction, 0));
        }

        private void OnRunActivated(object recipient, RunSelectedMessage msg)
        {
            var run = msg.SelectedRuns.First();
            if (cache.TryGetValue(run.RunId, out var vertices))
            {
                RunGraph.ClearState();
                verticesStates = vertices;
                ProcessToFraction();
                return;
            }
            this.RaisePropertyChanged(nameof(RunGraph));
            var rangeMsg = new QueryPathfindingRangeMessage();
            messenger.Send(rangeMsg);
            var rangeCoordinates = rangeMsg.PathfindingRange;
            var range = rangeCoordinates.Select(RunGraph.Get).ToArray();

            var subAlgorithms = new List<SubAlgorithmModel>();
            var visitedVertices = new List<(Coordinate Visited,
                IReadOnlyList<Coordinate> Enqueued)>();

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
                    algorithm.FindPath();
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
            RunGraph.ClearState();
            verticesStates = RunGraph.ToVerticesStates(subAlgorithms, rangeCoordinates);
            cache[run.RunId] = verticesStates;
            ProcessToFraction();
        }
    }

    enum RunState { No, Source, Target, Transit, Visited, Enqueued, Path, CrossPath }

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

        public static List<VertexState> ToVerticesStates(
            this IGraph<RunVertexModel> graph,
            IEnumerable<SubAlgorithmModel> subAlgorithms,
            IReadOnlyCollection<Coordinate> range)
        {
            var vertices = new List<VertexState>();

            vertices.Add(new(graph.Get(range.First()), RunState.Source, true));
            foreach (var transit in range.Skip(1).Take(range.Count - 2))
            {
                vertices.Add(new(graph.Get(transit), RunState.Transit, true));
            }
            vertices.Add(new(graph.Get(range.Last()), RunState.Target, true));

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
                        vertices.Add(new(graph.Get(enqueued), RunState.Visited, false));
                    }
                    foreach (var visited in Visited.Enumerate().Except(visitedIgnore).Distinct())
                    {
                        vertices.Add(new(graph.Get(visited), RunState.Visited, true));
                    }
                    foreach (var enqueued in Enqueued.Except(visitedIgnore).Except(previousEnqueued))
                    {
                        vertices.Add(new(graph.Get(enqueued), RunState.Enqueued, true));
                    }
                }
                var exceptRangePath = subAlgorithm.Path.Except(range).ToArray();
                foreach (var path in exceptRangePath.Intersect(previousPaths))
                {
                    vertices.Add(new(graph.Get(path), RunState.CrossPath, true));
                }
                foreach (var path in exceptRangePath.Except(previousPaths))
                {
                    vertices.Add(new(graph.Get(path), RunState.Path, true));
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

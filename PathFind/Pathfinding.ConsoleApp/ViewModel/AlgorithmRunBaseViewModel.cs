using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal abstract class AlgorithmRunBaseViewModel : BaseViewModel
    {
        protected enum RunState { Source, Target, Transit, Visited, Enqueued, Path, CrossPath }

        protected readonly IMessenger messenger;

        private IReadOnlyCollection<RunVertexModel> graphState = Array.Empty<RunVertexModel>();
        public IReadOnlyCollection<RunVertexModel> GraphState
        {
            get => graphState;
            set => this.RaiseAndSetIfChanged(ref graphState, value);
        }

        protected IGraph<GraphVertexModel> Graph { get; set; }

        protected Stack<(RunVertexModel Vertex, RunState State, bool Value)> Processed { get; set; } = new();

        protected Stack<(RunVertexModel Vertex, RunState State, bool Value)> Vertices { get; set; } = new();

        public ReactiveCommand<int, bool> ProcessNextCommand { get; }

        public ReactiveCommand<int, bool> ReverseNextCommand { get; }

        protected AlgorithmRunBaseViewModel(IMessenger messenger)
        {
            ProcessNextCommand = ReactiveCommand.Create<int, bool>(ProcessNext);
            ReverseNextCommand = ReactiveCommand.Create<int, bool>(ReverseNext);
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
            this.messenger = messenger;
        }

        protected Stack<(RunVertexModel Vertex, RunState State, bool Value)> GetVerticesStates(
            IEnumerable<SubAlgorithmModel> subAlgorithms,
            IReadOnlyCollection<Coordinate> range,
            IReadOnlyDictionary<Coordinate, RunVertexModel> graph)
        {
            Processed.Clear();
            var vertices = new Queue<(RunVertexModel Vertex, RunState State, bool Value)>();

            vertices.Enqueue((graph[range.First()], RunState.Source, true));
            foreach (var transit in range.Skip(1).Take(range.Count - 2))
            {
                vertices.Enqueue((graph[transit], RunState.Transit, true));
            }
            vertices.Enqueue((graph[range.Last()], RunState.Target, true));

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
                        vertices.Enqueue((graph[enqueued], RunState.Visited, false));
                    }
                    foreach (var visited in Visited.Enumerate().Except(visitedIgnore))
                    {
                        vertices.Enqueue((graph[visited], RunState.Visited, true));
                    }
                    foreach (var enqueued in Enqueued.Except(visitedIgnore).Except(previousEnqueued))
                    {
                        vertices.Enqueue((graph[enqueued], RunState.Enqueued, true));
                    }
                }
                var exceptRangePath = subAlgorithm.Path.Except(range).ToArray();
                foreach (var path in exceptRangePath.Intersect(previousPaths))
                {
                    vertices.Enqueue((graph[path], RunState.CrossPath, true));
                }
                foreach (var path in exceptRangePath.Except(previousPaths))
                {
                    vertices.Enqueue((graph[path], RunState.Path, true));
                }

                previousVisited.AddRange(subAlgorithm.Visited.Select(x => x.Visited));
                previousEnqueued.AddRange(subAlgorithm.Visited.SelectMany(x => x.Enqueued));
                previousPaths.AddRange(subAlgorithm.Path);
            }
            return new(vertices.Reverse());
        }

        private void SetState((RunVertexModel Vertex, RunState State, bool Value) tuple)
        {
            switch (tuple.State)
            {
                case RunState.Visited: tuple.Vertex.IsVisited = tuple.Value; break;
                case RunState.Enqueued: tuple.Vertex.IsEnqueued = tuple.Value; break;
                case RunState.CrossPath: tuple.Vertex.IsCrossedPath = tuple.Value; break;
                case RunState.Path: tuple.Vertex.IsPath = tuple.Value; break;
                case RunState.Source: tuple.Vertex.IsSource = tuple.Value; break;
                case RunState.Target: tuple.Vertex.IsTarget = tuple.Value; break;
                case RunState.Transit: tuple.Vertex.IsTransit = tuple.Value; break;
            }
        }

        private void OnGraphActivated(object recipient, GraphActivatedMessage msg)
        {
            Graph = msg.Graph.Graph;
        }

        private bool ProcessNext(int number)
        {
            while (number-- > 0 && Vertices.Count > 0)
            {
                var state = Vertices.Pop();
                Processed.Push((state.Vertex, state.State, !state.Value));
                SetState(state);
            }
            return Vertices.Count > 0;
        }

        private bool ReverseNext(int number)
        {
            while (number-- > 0 && Processed.Count > 0)
            {
                var state = Processed.Pop();
                Vertices.Push((state.Vertex, state.State, !state.Value));
                SetState(state);
            }
            return Processed.Count > 0;
        }

        protected Dictionary<Coordinate, RunVertexModel> CreateGraph()
        {
            var result = new Dictionary<Coordinate, RunVertexModel>();
            foreach (var vertex in Graph)
            {
                var runVertex = new RunVertexModel(vertex.Position)
                {
                    Cost = new VertexCost(vertex.Cost.CurrentCost, vertex.Cost.CostRange),
                    IsObstacle = vertex.IsObstacle
                };
                result.Add(runVertex.Position, runVertex);
            }
            foreach (var vertex in Graph)
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
    }
}

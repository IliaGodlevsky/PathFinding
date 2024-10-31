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
        protected readonly IMessenger messenger;

        private IReadOnlyCollection<RunVertexModel> graphState = Array.Empty<RunVertexModel>();
        public IReadOnlyCollection<RunVertexModel> GraphState
        {
            get => graphState;
            set => this.RaiseAndSetIfChanged(ref graphState, value);
        }

        protected IGraph<GraphVertexModel> Graph { get; set; }

        protected Stack<Action<bool>> Processed { get; set; } = new();

        protected Stack<Action<bool>> Vertices { get; set; } = new();

        public ReactiveCommand<int, bool> ProcessNextCommand { get; }

        public ReactiveCommand<int, bool> ReverseNextCommand { get; }

        protected AlgorithmRunBaseViewModel(IMessenger messenger)
        {
            ProcessNextCommand = ReactiveCommand.Create<int, bool>(ProcessNext);
            ReverseNextCommand = ReactiveCommand.Create<int, bool>(ReverseNext);
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
            this.messenger = messenger;
        }

        protected Stack<Action<bool>> GetVerticesStates(
            IEnumerable<SubAlgorithmModel> subAlgorithms,
            IReadOnlyCollection<Coordinate> range,
            IReadOnlyDictionary<Coordinate, RunVertexModel> graph)
        {
            Processed.Clear();
            var vertices = new Queue<Action<bool>>();
            //TODO: Add comments, cause this algorithm requires an explanation
            range.Skip(1).Take(range.Count - 2)
                .ForEach(transit => vertices.Enqueue(x => graph[transit].IsTransit = x));
            vertices.Enqueue(x => graph[range.First()].IsSource = x);
            vertices.Enqueue(x => graph[range.Last()].IsTarget = x);
            var previousVisited = new HashSet<Coordinate>();
            var previousPaths = new HashSet<Coordinate>();
            var previousEnqueued = new HashSet<Coordinate>();
            foreach (var subAlgorithm in subAlgorithms)
            {
                var visitedIgnore = range.Concat(previousPaths).ToArray();
                foreach (var (Visited, Enqueued) in subAlgorithm.Visited)
                {
                    Enqueued.Intersect(previousVisited)
                        .Except(visitedIgnore)
                        .ForEach(x => vertices.Enqueue(z => graph[x].IsVisited = !z));
                    var intersect = Enqueued.Intersect(previousEnqueued)
                        .Except(visitedIgnore)
                        .ForEach(x => vertices.Enqueue(z => graph[x].IsVisited = !z))
                        .ToList();
                    Visited.Enumerate()
                        .Except(visitedIgnore)
                        .ForEach(x => vertices.Enqueue(z => graph[x].IsVisited = z));
                    Enqueued.Except(visitedIgnore)
                        .Except(intersect)
                        .ForEach(enqueued => vertices.Enqueue(x => graph[enqueued].IsEnqueued = x));
                }
                previousVisited.AddRange(subAlgorithm.Visited.Select(x => x.Visited));
                previousEnqueued.AddRange(subAlgorithm.Visited.SelectMany(x => x.Enqueued));
                var exceptRangePath = subAlgorithm.Path.Except(range).ToArray();
                exceptRangePath
                    .Intersect(previousPaths)
                    .ForEach(y => vertices.Enqueue(z => graph[y].IsCrossedPath = z));
                exceptRangePath
                    .Except(previousPaths)
                    .ForEach(y => vertices.Enqueue(z => graph[y].IsPath = z));
                previousPaths.AddRange(subAlgorithm.Path);
            }
            return new(vertices.Reverse());
        }

        private void OnGraphActivated(object recipient, GraphActivatedMessage msg)
        {
            Graph = msg.Graph.Graph;
        }

        private bool ProcessNext(int number)
        {
            while (number-- > 0 && Vertices.Count > 0)
            {
                var action = Vertices.Pop();
                Processed.Push(action);
                action(true);
            }
            return Vertices.Count > 0;
        }

        private bool ReverseNext(int number)
        {
            while (number-- > 0 && Processed.Count > 0)
            {
                var action = Processed.Pop();
                Vertices.Push(action);
                action(false);
            }
            return Processed.Count > 0;
        }

        protected Dictionary<Coordinate, RunVertexModel> CreateGraph()
        {
            var result = new Dictionary<Coordinate, RunVertexModel>();
            foreach (var vertex in Graph)
            {
                var runVertex = new RunVertexModel()
                {
                    Position = vertex.Position,
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

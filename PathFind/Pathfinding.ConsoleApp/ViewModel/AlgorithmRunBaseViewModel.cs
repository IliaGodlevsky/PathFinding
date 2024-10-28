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
using System.Reactive;

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

        protected Queue<Action> Vertices { get; set; } = new();

        public int Remained => Vertices.Count;

        public ReactiveCommand<int, Unit> ProcessNextCommand { get; }

        protected AlgorithmRunBaseViewModel(IMessenger messenger)
        {
            ProcessNextCommand = ReactiveCommand.Create<int>(ProcessNext);
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
            this.messenger = messenger;
        }

        protected Queue<Action> GetVerticesStates(IEnumerable<SubAlgorithmModel> subAlgorithms,
            IReadOnlyCollection<Coordinate> range,
            IReadOnlyDictionary<Coordinate, RunVertexModel> graph)
        {
            var vertices = new Queue<Action>();
            range.Skip(1).Take(range.Count - 2)
                .ForEach(transit => vertices.Enqueue(() => graph[transit].IsTransit = true));
            vertices.Enqueue(() => graph[range.First()].IsSource = true);
            vertices.Enqueue(() => graph[range.Last()].IsTarget = true);
            foreach (var sub in subAlgorithms)
            {
                foreach (var (Visited, Enqueued) in sub.Visited)
                {
                    vertices.Enqueue(() => graph[Visited].IsVisited = true);
                    Enqueued.ForEach(enqueued => vertices.Enqueue(() => graph[enqueued].IsEnqueued = true));
                }
                sub.Path.ForEach(path => vertices.Enqueue(() => graph[path].IsPath = true));
            }
            return vertices;
        }

        private void OnGraphActivated(object recipient, GraphActivatedMessage msg)
        {
            Graph = msg.Graph.Graph;
        }

        private void ProcessNext(int number)
        {
            while (number-- > 0 && Remained > 0) Vertices.Dequeue().Invoke();
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

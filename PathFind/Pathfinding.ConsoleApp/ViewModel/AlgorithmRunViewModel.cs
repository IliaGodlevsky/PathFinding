using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Service.Interface;
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
    internal sealed class AlgorithmRunViewModel : ReactiveObject
    {
        private readonly IMessenger messenger;
        private readonly IRequestService<VertexModel> service;

        private IReadOnlyCollection<RunVertexModel> graphState = Array.Empty<RunVertexModel>();
        public IReadOnlyCollection<RunVertexModel> GraphState 
        {
            get => graphState;
            set => this.RaiseAndSetIfChanged(ref graphState, value);
        }

        public Queue<RunVisualizationVertex> Vertices { get; } = new();

        public int Remained => Vertices.Count;

        public ReactiveCommand<Unit, Unit> VisualizeNextCommand { get; }

        public AlgorithmRunViewModel([KeyFilter(KeyFilters.ViewModels)]IMessenger messenger,
            IRequestService<VertexModel> service)
        {
            this.messenger = messenger;
            this.service = service;
            messenger.Register<RunCreatedMessaged>(this, OnRunCreated);
            messenger.Register<RunActivatedMessage>(this, OnRunActivated);
            VisualizeNextCommand = ReactiveCommand.Create(VisualizeNext);
        }

        private void OnRunCreated(object recipient, RunCreatedMessaged msg)
        {
            var graphState = msg.Model.GraphState;
            GraphState = GenerateVerticesToVisualize(msg.Model.GraphState,
                msg.Model.SubAlgorithms);
        }

        private void OnRunActivated(object sender, RunActivatedMessage msg)
        {
            Vertices.Clear();
            GraphState = GenerateVerticesToVisualize(msg.Run.GraphState,
                msg.Run.Algorithms);
        }

        private IReadOnlyCollection <RunVertexModel> GenerateVerticesToVisualize(GraphStateModel graphState,
            IEnumerable<SubAlgorithmModel> subAlgorithms)
        {
            var graph = CreateGraph(graphState);
            var range = graphState.Range.ToList();
            range.Skip(1).Take(range.Count - 2)
                .ForEach(x => Vertices.Enqueue(RunVisualizationVertex.GetTransit(graph[x])));
            Vertices.Enqueue(RunVisualizationVertex.GetSource(graph[range[0]]));
            Vertices.Enqueue(RunVisualizationVertex.GetTarget(graph[range[range.Count - 1]]));
            foreach (var sub in subAlgorithms)
            {
                foreach (var visited in sub.Visited)
                {
                    Vertices.Enqueue(RunVisualizationVertex.GetVisited(graph[visited.Visited]));
                    visited.Enqueued.ForEach(x => Vertices.Enqueue(RunVisualizationVertex.GetEnqueued(graph[x])));
                }
                sub.Path.ForEach(x => Vertices.Enqueue(RunVisualizationVertex.GetPath(graph[x])));
            }
            return graph.Values;
        }

        private void VisualizeNext()
        {
            if (Vertices.Count > 0)
            {
                var toVisualize = Vertices.Dequeue();
                toVisualize.SetVisualizationFlag();
            }
        }

        private Dictionary<Coordinate, RunVertexModel> CreateGraph(GraphStateModel model)
        {
            var graph = model.Costs
                .ToDictionary(x => x.Position, x => x.Cost);
            var obstacles = model.Obstacles
                .Select(x => new RunVertexModel
                {
                    Position = x,
                    IsObstacle = true,
                    Cost = graph[x]
                });
            var regulars = model.Regulars
                .Select(x => new RunVertexModel
                {
                    Position = x,
                    IsObstacle = false,
                    Cost = graph[x]
                });
            return obstacles.Concat(regulars)
                .ToDictionary(x => x.Position, x => x);
        }
    }
}

using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal sealed class AlgorithmRunViewModel : ReactiveObject
    {
        internal enum PathfindingStatus { Visited, Enqueued, Source, Transit, Target, Path }

        private IReadOnlyCollection<RunVertexModel> graphState = Array.Empty<RunVertexModel>();
        public IReadOnlyCollection<RunVertexModel> GraphState
        {
            get => graphState;
            set => this.RaiseAndSetIfChanged(ref graphState, value);
        }

        public Queue<(RunVertexModel Model, PathfindingStatus Status)> Vertices { get; } = new();

        public ReactiveCommand<int, Unit> VisualizeNextCommand { get; }

        public AlgorithmRunViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger)
        {
            messenger.Register<RunCreatedMessaged>(this, OnRunCreated);
            messenger.Register<RunActivatedMessage>(this, OnRunActivated);
            VisualizeNextCommand = ReactiveCommand.Create<int>(VisualizeNext);
        }

        private void OnRunCreated(object recipient, RunCreatedMessaged msg)
        {
            GraphState = GenerateVerticesToVisualize(msg.Model.GraphState,
                msg.Model.SubAlgorithms);
        }

        private void OnRunActivated(object recipient, RunActivatedMessage msg)
        {
            Vertices.Clear();
            GraphState = GenerateVerticesToVisualize(msg.Run.GraphState,
                msg.Run.Algorithms);
        }

        private IReadOnlyCollection<RunVertexModel> GenerateVerticesToVisualize(GraphStateModel graphState,
            IEnumerable<SubAlgorithmModel> subAlgorithms)
        {
            var graph = CreateGraph(graphState);
            var range = graphState.Range.ToList();
            range.Skip(1).Take(range.Count - 2)
                .ForEach(transit => Vertices.Enqueue((graph[transit], PathfindingStatus.Transit)));
            Vertices.Enqueue((graph[range[0]], PathfindingStatus.Source));
            Vertices.Enqueue((graph[range[range.Count - 1]], PathfindingStatus.Target));
            foreach (var sub in subAlgorithms)
            {
                foreach (var (Visited, Enqueued) in sub.Visited)
                {
                    Vertices.Enqueue((graph[Visited], PathfindingStatus.Visited));
                    Enqueued.ForEach(enqueued => Vertices.Enqueue((graph[enqueued], PathfindingStatus.Enqueued)));
                }
                sub.Path.ForEach(path => Vertices.Enqueue((graph[path], PathfindingStatus.Path)));
            }
            return graph.Values;
        }

        private void VisualizeNext(int number)
        {
            while (number-- > 0 && Vertices.Count > 0)
            {
                var toVisualize = Vertices.Dequeue();
                var model = toVisualize.Model;
                switch (toVisualize.Status)
                {
                    case PathfindingStatus.Transit: model.IsTransit = true; break;
                    case PathfindingStatus.Source: model.IsSource = true; break;
                    case PathfindingStatus.Path: model.IsPath = true; break;
                    case PathfindingStatus.Target: model.IsTarget = true; break;
                    case PathfindingStatus.Enqueued: model.IsEnqueued = true; break;
                    case PathfindingStatus.Visited: model.IsVisited = true; break;
                }
            }
        }

        private Dictionary<Coordinate, RunVertexModel> CreateGraph(GraphStateModel model)
        {
            var graph = model.Costs.ToDictionary(x => x.Position, x => x.Cost);
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
            return obstacles.Concat(regulars).ToDictionary(x => x.Position, x => x);
        }
    }
}

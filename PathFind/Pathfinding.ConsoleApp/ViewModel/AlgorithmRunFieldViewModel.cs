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
    internal sealed class AlgorithmRunFieldViewModel : ReactiveObject
    {
        private IReadOnlyCollection<RunVertexModel> graphState = Array.Empty<RunVertexModel>();
        public IReadOnlyCollection<RunVertexModel> GraphState
        {
            get => graphState;
            set => this.RaiseAndSetIfChanged(ref graphState, value);
        }

        private Queue<Action> Vertices { get; } = new();

        public int Remained => Vertices.Count;

        public ReactiveCommand<int, Unit> ProcessNextCommand { get; }

        public AlgorithmRunFieldViewModel([KeyFilter(KeyFilters.ViewModels)] IMessenger messenger)
        {
            messenger.Register<RunCreatedMessaged>(this, OnRunCreated);
            messenger.Register<RunActivatedMessage>(this, OnRunActivated);
            ProcessNextCommand = ReactiveCommand.Create<int>(ProcessNext);
        }

        private void OnRunCreated(object recipient, RunCreatedMessaged msg)
        {
            GraphState = GenerateVerticesToVisualize(msg.Model.GraphState, msg.Model.SubAlgorithms);
        }

        private void OnRunActivated(object recipient, RunActivatedMessage msg)
        {
            GraphState = GenerateVerticesToVisualize(msg.Run.GraphState, msg.Run.SubAlgorithms);
        }

        private IReadOnlyCollection<RunVertexModel> GenerateVerticesToVisualize(
            GraphStateModel graphState,
            IEnumerable<SubAlgorithmModel> subAlgorithms)
        {
            Vertices.Clear();
            var graph = CreateGraph(graphState);
            var range = graphState.Range.ToArray();
            range.Skip(1).Take(range.Length - 2)
                .ForEach(transit => Vertices.Enqueue(() => graph[transit].IsTransit = true));
            Vertices.Enqueue(() => graph[range[0]].IsSource = true);
            Vertices.Enqueue(() => graph[range[range.Length - 1]].IsTarget = true);
            foreach (var sub in subAlgorithms)
            {
                foreach (var (Visited, Enqueued) in sub.Visited)
                {
                    Vertices.Enqueue(() => graph[Visited].IsVisited = true);
                    Enqueued.ForEach(enqueued => Vertices.Enqueue(() => graph[enqueued].IsEnqueued = true));
                }
                sub.Path.ForEach(path => Vertices.Enqueue(() => graph[path].IsPath = true));
            }
            return graph.Values;
        }

        private void ProcessNext(int number)
        {
            while (number-- > 0 && Remained > 0) Vertices.Dequeue().Invoke();
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

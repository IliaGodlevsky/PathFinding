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
using static Terminal.Gui.View;

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

        public Queue<VisualizationPair> VisualizationPipeline { get; } = new();

        public int Remained => VisualizationPipeline.Count;

        public ReactiveCommand<MouseEventArgs, Unit> VisualizeNextCommand { get; }

        public AlgorithmRunViewModel([KeyFilter(KeyFilters.ViewModels)]IMessenger messenger,
            IRequestService<VertexModel> service)
        {
            this.messenger = messenger;
            this.service = service;
            messenger.Register<RunCreatedMessaged>(this, OnRunCreated);
            VisualizeNextCommand = ReactiveCommand.Create<MouseEventArgs>(VisualizeNext);
        }

        private void OnRunCreated(object recipient, RunCreatedMessaged msg)
        {
            var graphState = msg.Model.GraphState;
            var graph = CreateGraph(msg.Model.GraphState);
            GraphState = graph.Values;
            var range = graphState.Range.ToList();
            range.Skip(1).Take(range.Count - 2)
                .ForEach(x => AddTransitToPipeline(graph, x));
            AddSourceToPipeline(graph, range[0]);
            AddTargetToPipeline(graph, range[range.Count - 1]);
            foreach (var sub in msg.Model.SubAlgorithms)
            {
                foreach (var visited in sub.Visited)
                {
                    AddVisitedToPipeline(graph, visited.Visited);
                    visited.Enqueued.ForEach(x => AddEnqueuedToPipeline(graph, x));
                }
                sub.Path.ForEach(x => AddPathToPipeline(graph, x));
            }
        }

        private void VisualizeNext(EventArgs e)
        {
            var toVisualize = VisualizationPipeline.Dequeue();
            toVisualize.Action(toVisualize.Vertex);
        }

        private void AddPathToPipeline(Dictionary<Coordinate, RunVertexModel> graph,
            Coordinate coordinate)
        {
            AddToPipeline(graph, coordinate, x => x.IsPath = true);
        }

        private void AddSourceToPipeline(Dictionary<Coordinate, RunVertexModel> graph,
            Coordinate coordinate)
        {
            AddToPipeline(graph, coordinate, x => x.IsSource = true);
        }

        private void AddTargetToPipeline(Dictionary<Coordinate, RunVertexModel> graph,
            Coordinate coordinate)
        {
            AddToPipeline(graph, coordinate, x => x.IsTarget = true);
        }

        private void AddTransitToPipeline(Dictionary<Coordinate, RunVertexModel> graph,
            Coordinate coordinate)
        {
            AddToPipeline(graph, coordinate, x => x.IsTransit = true);
        }

        private void AddVisitedToPipeline(Dictionary<Coordinate, RunVertexModel> graph,
            Coordinate coordinate)
        {
            AddToPipeline(graph, coordinate, x => x.IsVisited = true);
        }

        private void AddEnqueuedToPipeline(Dictionary<Coordinate, RunVertexModel> graph,
            Coordinate coordinate)
        {
            AddToPipeline(graph, coordinate, x => x.IsEnqueued = true);
        }

        private void AddToPipeline(Dictionary<Coordinate, RunVertexModel> graph,
            Coordinate coordinate, Action<RunVertexModel> action)
        {
            VisualizationPipeline.Enqueue(new()
            {
                Vertex = graph[coordinate],
                Action = action
            });
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

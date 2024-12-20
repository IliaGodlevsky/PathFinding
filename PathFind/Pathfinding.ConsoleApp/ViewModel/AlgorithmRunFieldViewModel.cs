﻿using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.ConsoleApp.Messages.ViewModel;
using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.ViewModel.Interface;
using Pathfinding.Domain.Core;
using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Infrastructure.Business.Builders;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Logging.Interface;
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
        private List<VertexState> selectedRun = new();

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
            this.log = log;
            this.messenger = messenger;
            this.graphAssemble = graphAssemble;
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
            messenger.Register<GraphStateChangedMessage>(this, OnGraphStatusChanged);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
            messenger.Register<GraphUpdatedMessage>(this, OnGraphUpdated);
            messenger.Register<RunSelectedMessage>(this, OnRunActivated);
            ProcessNextCommand = ReactiveCommand.Create<float>(ProcessNext);
            ReverseNextCommand = ReactiveCommand.Create<float>(ReverseNext);
            ProcessToCommand = ReactiveCommand.Create<float>(ProcessTo);
        }

        private void OnRunActivated(object recipient, RunSelectedMessage msg)
        {
            if (msg.SelectedRuns.Length > 0)
            {
                ActivateRun(msg.SelectedRuns[0]);
            }
        }

        private void OnGraphStatusChanged(object recipient, GraphStateChangedMessage msg)
        {
            if (msg.Status == GraphStatuses.Editable && graphId == msg.Id)
            {
                Clear();
            }
        }

        private void OnGraphUpdated(object recipient, GraphUpdatedMessage msg)
        {
            this.RaisePropertyChanged(nameof(RunGraph));
        }

        private void Clear()
        {
            cache.Clear();
            Cursor = 0;
            Fraction = 0;
            selectedRun = new();
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
            var graphVertices = msg.Graph.Vertices;
            var graph = new Graph<GraphVertexModel>(msg.Graph.Vertices,
                msg.Graph.DimensionSizes);
            var runGraph = graphAssemble.AssembleGraph(
                graph,
                msg.Graph.DimensionSizes);
            RunGraph = runGraph;
            disposables.Clear();
            foreach (var vertex in graphVertices)
            {
                var runVertex = runGraph.Get(vertex.Position);
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
            ProcessForward((int)Math.Round(selectedRun.Count * fraction, 0));
            if (Cursor > selectedRun.Count) Cursor = selectedRun.Count;
        }

        private void ProcessForward(int count)
        {
            while (count-- > 0 && Cursor < selectedRun.Count)
            {
                selectedRun.ElementAtOrDefault(Cursor++).SetState();
            }
        }

        private void ProcessBackward(int count)
        {
            while (count-- > 0 && Cursor > 0)
            {
                selectedRun.ElementAtOrDefault(--Cursor).ToReverted().SetState();
            }
        }

        private void ProcessTo(float fraction)
        {
            int count = (int)Math.Round(selectedRun.Count * fraction, 0);
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
            ProcessBackward((int)Math.Round(selectedRun.Count * fraction, 0));
            if (Cursor < 0) Cursor = 0;
        }

        private void ProcessToFraction()
        {
            Cursor = 0;
            ProcessForward((int)Math.Round(selectedRun.Count * Fraction, 0));
        }

        private void ActivateRun(RunInfoModel model)
        {
            var run = model;
            if (cache.TryGetValue(run.Id, out var vertices))
            {
                RunGraph.ClearState();
                selectedRun = vertices;
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
                var algorithm = AlgorithmBuilder
                    .TakeAlgorithm(run.Algorithm)
                    .WithAlgorithmInfo(run)
                    .Build(range);

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
            var verticesStates = RunGraph.ToVerticesStates(subAlgorithms, rangeCoordinates);
            selectedRun = verticesStates;
            cache[run.Id] = verticesStates;
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
            var vertices = new List<VertexState>
            {
                new(graph.Get(range.First()), RunState.Source, true)
            };
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
    }
}

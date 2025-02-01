﻿using Autofac.Features.AttributeFilters;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using Pathfinding.App.Console.Injection;
using Pathfinding.App.Console.Messages.ViewModel;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.ViewModel.Interface;
using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Business.Algorithms.Events;
using Pathfinding.Infrastructure.Business.Builders;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using static Pathfinding.App.Console.Model.AlgorithmRevisionModel;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class AlgorithmRunFieldViewModel : BaseViewModel, IAlgorithmRunFieldViewModel
    {
        private readonly IMessenger messenger;
        private readonly IGraphAssemble<RunVertexModel> graphAssemble;

        private readonly CompositeDisposable disposables = [];

        private int graphId;

        private AlgorithmRevisionModel selected = Empty;
        public AlgorithmRevisionModel SelectedRun
        {
            get => selected;
            set
            {
                float prevFraction = selected.Fraction;
                selected.Fraction = 0;
                this.RaiseAndSetIfChanged(ref selected, value);
                selected.Fraction = prevFraction;
            }
        }

        public ObservableCollection<AlgorithmRevisionModel> Runs { get; } = [];

        public IGraph<RunVertexModel> RunGraph { get; private set; } = Graph<RunVertexModel>.Empty;

        public AlgorithmRunFieldViewModel(
            IGraphAssemble<RunVertexModel> graphAssemble,
            [KeyFilter(KeyFilters.ViewModels)] IMessenger messenger)
        {
            this.messenger = messenger;
            this.graphAssemble = graphAssemble;
            Runs.ActOnEveryObject(OnAdded, OnRemoved);
            messenger.Register<GraphActivatedMessage>(this, OnGraphActivated);
            messenger.Register<RunsDeletedMessage>(this, OnRunsDeleted);
            messenger.Register<GraphsDeletedMessage>(this, OnGraphDeleted);
            messenger.Register<GraphUpdatedMessage>(this, OnGraphUpdated);
            messenger.Register<RunSelectedMessage>(this, OnRunActivated);
        }

        private void OnRunActivated(object recipient, RunSelectedMessage msg)
        {
            if (msg.SelectedRuns.Length > 0)
            {
                ActivateRun(msg.SelectedRuns[0]);
            }
        }

        private void OnRunsDeleted(object recipient, RunsDeletedMessage msg)
        {
            if (msg.RunIds.Contains(SelectedRun.Id))
            {
                selected = Empty;
            }
            var runs = Runs.Where(x => msg.RunIds.Contains(x.Id)).ToArray();
            Runs.Remove(runs);
        }

        private void OnGraphUpdated(object recipient, GraphUpdatedMessage msg)
        {
            this.RaisePropertyChanged(nameof(RunGraph));
        }

        private void Clear()
        {
            Runs.Clear();
            SelectedRun.Fraction = 0;
            selected = Empty;
            disposables.Clear();
        }

        private void OnGraphDeleted(object recipient, GraphsDeletedMessage msg)
        {
            if (msg.GraphIds.Contains(graphId))
            {
                graphId = 0;
                RunGraph = Graph<RunVertexModel>.Empty;
                this.RaisePropertyChanged(nameof(RunGraph));
                Clear();
            }
        }

        private void OnGraphActivated(object recipient, GraphActivatedMessage msg)
        {
            graphId = msg.Graph.Id;
            var graphVertices = msg.Graph.Vertices;
            var dimensions = msg.Graph.DimensionSizes;
            var graph = new Graph<GraphVertexModel>(graphVertices, dimensions);
            var runGraph = graphAssemble.AssembleGraph(graph, dimensions);
            RunGraph = Graph<RunVertexModel>.Empty;
            this.RaisePropertyChanged(nameof(RunGraph));
            RunGraph = runGraph;
            Clear();
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
        }

        private void OnAdded(AlgorithmRevisionModel model) { }

        private void OnRemoved(AlgorithmRevisionModel model) => model.Dispose();

        private void ActivateRun(RunInfoModel model)
        {
            var run = Runs.FirstOrDefault(x => x.Id == model.Id);
            if (run == null)
            {
                this.RaisePropertyChanged(nameof(RunGraph));

                var rangeMsg = new QueryPathfindingRangeMessage();
                messenger.Send(rangeMsg);
                var rangeCoordinates = rangeMsg.PathfindingRange;
                var range = rangeCoordinates.Select(RunGraph.Get).ToArray();

                var subRevisions = new List<SubRevisionModel>();
                var visitedVertices = new List<VisitedModel>();

                void AddSubAlgorithm(IReadOnlyCollection<Coordinate> path = null)
                {
                    subRevisions.Add(new(visitedVertices, path ?? []));
                    visitedVertices = [];
                }
                void OnVertexProcessed(object sender, VerticesProcessedEventArgs e)
                {
                    visitedVertices.Add(new(e.Current, e.Enqueued));
                }
                void OnSubPathFound(object sender, SubPathFoundEventArgs args)
                {
                    AddSubAlgorithm(args.SubPath);
                }

                var algorithm = AlgorithmBuilder.CreateAlgorithm(range, model);
                algorithm.SubPathFound += OnSubPathFound;
                algorithm.VertexProcessed += OnVertexProcessed;
                try
                {
                    algorithm.FindPath();
                }
                catch
                {
                    AddSubAlgorithm();
                }
                algorithm.SubPathFound -= OnSubPathFound;
                algorithm.VertexProcessed -= OnVertexProcessed;

                run = new AlgorithmRevisionModel(RunGraph,
                    subRevisions, rangeCoordinates) { Id = model.Id };
                Runs.Add(run);
            }
            SelectedRun = run;
        }
    }
}
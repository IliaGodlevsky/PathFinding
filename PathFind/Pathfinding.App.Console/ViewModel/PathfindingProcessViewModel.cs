using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Exceptions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Interface.Extensions;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Extensions;
using Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pathfinding.App.Console.ViewModel
{
    using AlgorithmFactory = IAlgorithmFactory<PathfindingProcess>;

    [MenuColumnsNumber(2)]
    internal sealed class PathfindingProcessViewModel : SafeParentViewModel, IRequireConsoleKeyInput
    {
        private readonly IMessenger messenger;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        private readonly Stopwatch timer;
        private readonly Queue<AlgorithmFactory> factories;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        private int visitedVerticesCount = 0;
        private IGraphPath path = NullGraphPath.Instance;
        private PathfindingProcess algorithm = PathfindingProcess.Null;

        public IInput<ConsoleKey> KeyInput { get; set; }

        private string Statistics => path.ToStatistics(timer, visitedVerticesCount, algorithm);
        
        public PathfindingProcessViewModel(IPathfindingRangeBuilder<Vertex> rangeBuilder, 
            IMessenger messenger, ILog log)
            : base(log)
        {
            this.factories = new Queue<AlgorithmFactory>();
            this.messenger = messenger;
            this.rangeBuilder = rangeBuilder;
            this.timer = new Stopwatch();
            messenger.Register<PathfindingAlgorithmChosenMessage>(this, OnAlgorithmChosen);
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        public override void Dispose()
        {
            base.Dispose();
            messenger.Unregister(this);
        }

        [MenuItem(MenuItemsNames.ChooseAlgorithm, 0)]
        private void ChooseAlgorithm() => Display<PathfindingProcessChooseViewModel>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(CanStartPathfinding))]
        [MenuItem(MenuItemsNames.FindPath, 1)]
        private void FindPath()
        {
            using (Cursor.HideCursor())
            {
                FindPathInternal();
            }
        }

        [MenuItem(MenuItemsNames.ClearColors, 4)]
        private void ClearColors()
        {
            messenger.Send(PathfindingStatisticsMessage.Empty);
            graph.RestoreVerticesVisualState();
            rangeBuilder.Range.RestoreVerticesVisualState();
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsVisualizationSupported))]
        [MenuItem(MenuItemsNames.CustomizeVisualization, 2)]
        private void CustomizeVisualization() => Display<PathfindingVisualizationViewModel>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsHistorySupported))]
        [MenuItem(MenuItemsNames.History, 3)]
        private void PathfindingHistory() => Display<PathfindingHistoryViewModel>();

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            visitedVerticesCount++;
            messenger.Send(new PathfindingStatisticsMessage(Statistics));
        }

        private void FindPathInternal()
        {
            while (CanStartPathfinding())
            {
                var factory = factories.Dequeue();
                using (algorithm = factory.Create(rangeBuilder.Range))
                {
                    try
                    {
                        using (Disposable.Use(SummarizeResults))
                        {
                            PrepareForPathfinding(algorithm);
                            path = algorithm.FindPath();
                            graph.GetVertices(path).VisualizeAsPath();
                        }
                        KeyInput.Input();
                        ClearColors();
                    }
                    catch (PathfindingException ex)
                    {
                        log.Debug(ex.Message);
                    }
                }
            }
        }

        private void OnAlgorithmChosen(PathfindingAlgorithmChosenMessage message)
        {
            factories.Enqueue(message.Algorithm);
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }

        private void SummarizeResults()
        {
            var statistics = path.Count > 0 ? Statistics : MessagesTexts.CouldntFindPathMsg;
            messenger.Send(new PathfindingStatisticsMessage(statistics));
            messenger.Send(new PathFoundMessage(path, algorithm));
            messenger.Send(new AlgorithmFinishedMessage(algorithm, statistics));
            visitedVerticesCount = 0;
            path = NullGraphPath.Interface;
            algorithm = PathfindingProcess.Null;
        }

        [FailMessage(MessagesTexts.NoAlgorithmChosenMsg)]
        private bool CanStartPathfinding() => factories.Count > 0;

        [FailMessage(MessagesTexts.OperationIsNotSupported)]
        private bool IsHistorySupported() => IsOperationSuppoted<PathfindingHistoryViewModel>();

        [FailMessage(MessagesTexts.OperationIsNotSupported)]
        private bool IsVisualizationSupported() => IsOperationSuppoted<PathfindingVisualizationViewModel>();

        private void PrepareForPathfinding(PathfindingProcess algorithm)
        {
            messenger.Send(new SubscribeOnVisualizationMessage(algorithm));
            messenger.Send(new SubscribeOnHistoryMessage(algorithm));
            messenger.Send(new PathfindingRangeChosenMessage(rangeBuilder.Range, algorithm));
            algorithm.VertexVisited += OnVertexVisited;
            algorithm.Finished += (s, e) => timer.Stop();
            algorithm.Started += (s, e) => timer.Restart();
            algorithm.Interrupted += (s, e) => timer.Stop();
            algorithm.Paused += (s, e) => timer.Stop();
            algorithm.Resumed += (s, e) => timer.Start();
        }
    }
}
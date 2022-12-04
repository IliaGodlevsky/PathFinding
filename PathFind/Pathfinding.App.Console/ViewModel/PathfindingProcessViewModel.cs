using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Exceptions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Interface.Extensions;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.DependencyInjection;
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
    [InstancePerLifetimeScope]
    internal sealed class PathfindingProcessViewModel : SafeViewModel, IRequireConsoleKeyInput
    {
        private static readonly TimeSpan StepDelay = TimeSpan.FromMilliseconds(0.5);

        private readonly IMessenger messenger;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        private readonly Stopwatch timer;
        private readonly ILifetimeScope lifetimeScope;
        private readonly Queue<AlgorithmFactory> factories;
        private readonly Graph2D<Vertex> graph;

        private int visitedVerticesCount = 0;
        private IGraphPath path = NullGraphPath.Instance;
        private PathfindingProcess algorithm = PathfindingProcess.Null;

        public IInput<ConsoleKey> KeyInput { get; set; }

        private string Statistics => path.ToStatistics(timer, visitedVerticesCount, algorithm);
        
        public PathfindingProcessViewModel(IPathfindingRangeBuilder<Vertex> rangeBuilder, 
            ICache<Graph2D<Vertex>> graph, IMessenger messenger, ILog log)
            : base(log)
        {
            this.factories = new Queue<AlgorithmFactory>();
            this.lifetimeScope = DI.Container.BeginLifetimeScope();
            this.messenger = messenger;
            this.graph = graph.Cached;
            this.rangeBuilder = rangeBuilder;
            this.timer = new Stopwatch();
            messenger.Register<PathfindingAlgorithmChosenMessage>(this, OnAlgorithmChosen);
        }

        public override void Dispose()
        {
            base.Dispose();
            lifetimeScope.Dispose();
            messenger.Unregister(this);
        }

        [MenuItem(MenuItemsNames.ChooseAlgorithm, 0)]
        private void ChooseAlgorithm() => DI.Container.Display<PathfindingProcessChooseViewModel>();

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

        [MenuItem(MenuItemsNames.CustomizeVisualization, 2)]
        private void CustomizeVisualization() => lifetimeScope.Display<PathfindingVisualizationViewModel>();

        [MenuItem(MenuItemsNames.History, 3)]
        private void PathfindingHistory() => lifetimeScope.Display<PathfindingHistoryViewModel>();

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
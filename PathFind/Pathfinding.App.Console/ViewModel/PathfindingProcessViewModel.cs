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
using Pathfinding.App.Console.EventArguments;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.App.Console.Views;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.Visualization.Extensions;
using Shared.Primitives;
using System;
using System.Diagnostics;
using System.Linq;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(2)]
    [InstancePerLifetimeScope]
    internal sealed class PathfindingProcessViewModel : SafeViewModel, IRequireConsoleKeyInput
    {
        private readonly IMessenger messenger;
        private readonly ConsoleKeystrokesHook keystrokesHook;
        private readonly PathfindingRangeAdapter<Vertex> adapter;
        private readonly Stopwatch timer;
        private readonly ILifetimeScope lifetimeScope;

        private int visitedVerticesCount;

        public IInput<ConsoleKey> KeyInput { get; set; }

        private IGraphPath Path { get; set; } = NullGraphPath.Instance;

        private Graph2D<Vertex> Graph { get; } = Graph2D<Vertex>.Empty;

        private PathfindingProcess Algorithm { get; set; }

        private IPathfindingRange PathfindingRange => adapter.GetPathfindingRange();

        private string Statistics => Path.ToStatistics(timer, visitedVerticesCount, Algorithm);

        private IAlgorithmFactory<PathfindingProcess> Factory { get; set; }

        public PathfindingProcessViewModel(PathfindingRangeAdapter<Vertex> adapter, ConsoleKeystrokesHook hook, 
            ICache<Graph2D<Vertex>> graph, IMessenger messenger, ILog log)
            : base(log)
        {
            lifetimeScope = DI.Container.BeginLifetimeScope();
            this.messenger = messenger;
            keystrokesHook = hook;
            Graph = graph.Cached;
            this.adapter = adapter;
            timer = new Stopwatch();
            Path = NullGraphPath.Interface;
            keystrokesHook.KeyPressed += OnConsoleKeyPressed;
            messenger.Register<PathfindingAlgorithmChosenMessage>(this, OnAlgorithmChosen);
        }

        public override void Dispose()
        {
            base.Dispose();
            lifetimeScope.Dispose();
            keystrokesHook.KeyPressed -= OnConsoleKeyPressed;
            messenger.Unregister(this);
        }

        [MenuItem(MenuItemsNames.ChooseAlgorithm, 0)]
        private void ChooseAlgorithm() => DI.Container.Display<PathfindingProcessChooseView>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(CanStartPathfinding))]
        [MenuItem(MenuItemsNames.FindPath, 1)]
        private void FindPath()
        {
            using (Cursor.HideCursor())
            {
                FindPathInternal();
                KeyInput.Input();
            }
        }

        [MenuItem(MenuItemsNames.ClearColors, 4)]
        private void ClearColors()
        {
            Graph.RestoreVerticesVisualState();
            adapter.RestoreVerticesVisualState();
        }

        [MenuItem(MenuItemsNames.CustomizeVisualization, 2)]
        private void CustomizeVisualization() => lifetimeScope.Display<PathfindingVisualizationView>();

        [MenuItem(MenuItemsNames.History, 3)]
        private void PathfindingHistory() => lifetimeScope.Display<PathfindingHistoryView>();

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            visitedVerticesCount++;
            messenger.Send(new UpdatePathfindingStatisticsMessage(Statistics));
        }

        private void FindPathInternal()
        {
            using (Algorithm = Factory.Create(PathfindingRange))
            {
                try
                {
                    using (Disposable.Use(SummarizeResults))
                    {
                        PrepareForPathfinding(Algorithm);
                        Path = Algorithm.FindPath();
                        Graph.GetVertices(Path).Reverse().VisualizeAsPath();
                    }
                }
                catch (PathfindingException ex)
                {
                    log.Debug(ex.Message);
                }
            }
        }

        private void OnAlgorithmChosen(PathfindingAlgorithmChosenMessage message)
        {
            Factory = message.Algorithm;
        }

        private void OnConsoleKeyPressed(object sender, ConsoleKeyPressedEventArgs e)
        {
            switch (e.PressedKey)
            {
                case ConsoleKey.Escape:
                    Algorithm.Interrupt();
                    break;
                case ConsoleKey.P:
                    Algorithm.Pause();
                    break;
                case ConsoleKey.Enter:
                    Algorithm.Resume();
                    break;
            }
        }

        private void SummarizeResults()
        {
            var statistics = Path.Count > 0 ? Statistics : MessagesTexts.CouldntFindPathMsg;
            messenger.Send(new UpdatePathfindingStatisticsMessage(statistics));
            messenger.Send(new PathFoundMessage(Path, Algorithm));
            messenger.Send(new AlgorithmFinishedMessage(Algorithm, statistics));
            visitedVerticesCount = 0;
        }

        [FailMessage(MessagesTexts.NoAlgorithmChosenMsg)]
        private bool CanStartPathfinding() => Factory is not null;

        private void PrepareForPathfinding(PathfindingProcess algorithm)
        {
            messenger.Send(new SubscribeOnVisualizationMessage(algorithm));
            messenger.Send(new SubscribeOnHistoryMessage(algorithm));
            messenger.Send(new PathfindingRangeChosenMessage(PathfindingRange, Algorithm));
            algorithm.VertexVisited += OnVertexVisited;
            algorithm.Finished += (s, e) => timer.Stop();
            algorithm.Started += (s, e) => timer.Restart();
            algorithm.Interrupted += (s, e) => timer.Stop();
            algorithm.Paused += (s, e) => timer.Stop();
            algorithm.Resumed += (s, e) => timer.Start();
            algorithm.Started += keystrokesHook.StartAsync;
            algorithm.Finished += (s, e) => keystrokesHook.Interrupt();
            
        }
    }
}
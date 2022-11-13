using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Exceptions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Interface.Extensions;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.EventArguments;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.MenuCommands.Attributes;
using Pathfinding.App.Console.Views;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.Visualization.Extensions;
using Shared.Primitives;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(2)]
    internal sealed class PathfindingProcessViewModel : SafeViewModel, IRequireConsoleKeyInput
    {
        private readonly IMessenger messenger;
        private readonly ConsoleKeystrokesHook keystrokesHook;
        private readonly VisualPathfindingRange<Vertex> range;
        private readonly Stopwatch timer;
        private readonly ManualResetEventSlim resetEvent;

        private int visitedVerticesCount;

        public IInput<ConsoleKey> KeyInput { get; set; }

        private IGraphPath Path { get; set; } = NullGraphPath.Instance;

        private Graph2D<Vertex> Graph { get; } = Graph2D<Vertex>.Empty;

        private PathfindingProcess Algorithm { get; set; }

        private string Statistics => Path.ToStatistics(timer, visitedVerticesCount, Algorithm);

        private IAlgorithmFactory<PathfindingProcess> Factory { get; set; }

        public PathfindingProcessViewModel(VisualPathfindingRange<Vertex> range, ConsoleKeystrokesHook hook, 
            ICache<Graph2D<Vertex>> graph, IMessenger messenger, ILog log)
            : base(log)
        {
            this.messenger = messenger;
            resetEvent = new ManualResetEventSlim();
            keystrokesHook = hook;
            Graph = graph.Cached;
            this.range = range;
            timer = new Stopwatch();
            Path = NullGraphPath.Interface;
            keystrokesHook.KeyPressed += OnConsoleKeyPressed;
            messenger.Register<PathfindingAlgorithmChosen>(this, OnAlgorithmChosen);
        }

        public override void Dispose()
        {
            base.Dispose();
            keystrokesHook.KeyPressed -= OnConsoleKeyPressed;
            resetEvent.Dispose();
            messenger.Unregister(this);
        }

        [MenuItem(MenuItemsNames.ChooseAlgorithm, 0)]
        private void ChooseAlgorithm() => DI.Container.Display<PathfindingProcessChooseView>();

        [Condition(nameof(CanStartPathfinding))]
        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.FindPath, 1)]
        private void FindPath()
        {
            using (Cursor.HideCursor())
            {
#pragma warning disable CS4014
                FindPathInternal();
#pragma warning restore CS4014
                resetEvent.Reset();
                resetEvent.Wait();
                KeyInput.Input();
            }
        }

        [MenuItem(MenuItemsNames.ClearColors, 3)]
        private void ClearColors()
        {
            Graph.RestoreVerticesVisualState();
            range.RestoreVerticesVisualState();
        }

        [MenuItem(MenuItemsNames.CustomizeVisualization, 2)]
        private void CustomizeVisualization() => DI.Container.Display<PathfindingVisualizationView>();

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            visitedVerticesCount++;
            messenger.Send(new UpdateStatisticsMessage(Statistics));
        }

        private async Task FindPathInternal()
        {
            using (Algorithm = Factory.Create(range))
            {
                try
                {
                    using var _ = Disposable.Use(SummarizeResults);
                    SubscribeOnAlgorithmEvents(Algorithm);
                    messenger.Send(new SubscribeOnVisualizationMessage(Algorithm));
                    Path = await Algorithm.FindPathAsync();
                    await Path.Select(Graph.Get).VisualizeAsPathAsync();
                }
                catch (PathfindingException ex)
                {
                    log.Debug(ex.Message);
                }
            }
        }

        private void OnAlgorithmChosen(PathfindingAlgorithmChosen message)
        {
            Factory = message.Algorithm;
        }

        private void OnConsoleKeyPressed(object sender, ConsoleKeyPressedEventArgs e)
        {
            switch (e.PressedKey)
            {
                case ConsoleKey.Escape:
                    Algorithm?.Interrupt();
                    break;
                case ConsoleKey.P:
                    Algorithm?.Pause();
                    break;
                case ConsoleKey.Enter:
                    Algorithm?.Resume();
                    break;
            }
        }

        private void SummarizeResults()
        {
            var statistics = Path.Count > 0 ? Statistics : MessagesTexts.CouldntFindPathMsg;
            messenger.Send(new UpdateStatisticsMessage(statistics));
            visitedVerticesCount = 0;
            resetEvent.Set();
        }

        [FailMessage(MessagesTexts.NoAlgorithmChosen)]
        private bool CanStartPathfinding() => Factory is not null;

        private void SubscribeOnAlgorithmEvents(PathfindingProcess algorithm)
        {
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
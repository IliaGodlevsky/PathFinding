using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Exceptions;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Interface.Extensions;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.EventArguments;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Extensions;
using Shared.Primitives;
using Shared.Primitives.Attributes;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class PathfindingProcessViewModel : ViewModel, ICache<PathfindingProcess>, IRequireConsoleKeyInput
    {
        private readonly IMessenger messenger;
        private readonly IPathfindingRange range;
        private readonly Stopwatch timer;
        private readonly IAlgorithmFactory<PathfindingProcess> algorithmFactory;
        private readonly ManualResetEventSlim resetEvent;

        private int visitedVerticesCount;

        public IInput<ConsoleKey> KeyInput { get; set; }

        public ConsoleKeystrokesHook KeystrokesHook { get; set; }

        private IGraphPath Path { get; set; } = NullGraphPath.Instance;

        private Graph2D<Vertex> Graph { get; }

        private PathfindingProcess Algorithm { get; set; }

        PathfindingProcess ICache<PathfindingProcess>.Cached => Algorithm;

        private string Statistics => Path.ToStatistics(timer, visitedVerticesCount, Algorithm.ToString());

        public PathfindingProcessViewModel(IPathfindingRange range, ICache<IAlgorithmFactory<PathfindingProcess>> factory, 
            ICache<Graph2D<Vertex>> graph, IMessenger messenger, ILog log)
            : base(log)
        {
            Graph = graph.Cached;
            this.algorithmFactory = factory.Cached;
            this.range = range;
            timer = new Stopwatch();
            Path = NullGraphPath.Interface;
            KeystrokesHook.KeyPressed += OnConsoleKeyPressed;
        }

        public override void Dispose()
        {
            base.Dispose();
            KeystrokesHook.KeyPressed -= OnConsoleKeyPressed;
            resetEvent.Dispose();
        }

        [Order(0)]
        [MenuItem("Find path")]
        private void FindPath()
        {
            using (Cursor.HideCursor())
            {
                FindPathInternal();
                resetEvent.Reset();
                resetEvent.Wait();
                KeyInput.Input();
            }
        }

        [Order(1)]
        [MenuItem("Set visualization")]
        private void SetVisualization()
        {

        }

        [Order(2)]
        [MenuItem("Exit")]
        private void Exit()
        {

        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {           
            visitedVerticesCount++;
            messenger.Send(new UpdateStatisticsMessage(Statistics));
        }

        private async void FindPathInternal()
        {
            using (Algorithm = algorithmFactory.Create(range))
            {
                try
                {
                    using var _ = Disposable.Use(SummarizeResults);
                    SubscribeOnAlgorithmEvents(Algorithm);
                    Path = await Algorithm.FindPathAsync();
                    await Path.Select(Graph.Get).VisualizeAsPathAsync();
                }
                catch (PathfindingException ex)
                {
                    log.Debug(ex.Message);
                }
                catch (Exception ex)
                {
                    Algorithm.Interrupt();
                    log.Error(ex);
                }
            }
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
            string statistics = Path.Count == 0 ? MessagesTexts.CouldntFindPathMsg : Statistics;
            messenger.Send(new UpdateStatisticsMessage(statistics));
            visitedVerticesCount = 0;
            resetEvent.Set();
        }

        private void SubscribeOnAlgorithmEvents(PathfindingProcess algorithm)
        {
            algorithm.VertexVisited += OnVertexVisited;
            algorithm.Finished += (s, e) => timer.Stop();
            algorithm.Started += (s, e) => timer.Restart();
            algorithm.Interrupted += (s, e) => timer.Stop();
            algorithm.Paused += (s, e) => timer.Stop();
            algorithm.Resumed += (s, e) => timer.Start();
            algorithm.Started += KeystrokesHook.StartAsync;
            algorithm.Finished += (s, e) => KeystrokesHook.Interrupt();
        }
    }
}
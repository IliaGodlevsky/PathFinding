using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.Delegates;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.EventArguments;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Views;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.Visualization.Models;
using Shared.Extensions;
using Shared.Primitives.Attributes;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class PathFindingViewModel : PathFindingModel<Vertex>, IViewModel, IRequireTimeSpanInput, IRequireIntInput, IRequireAnswerInput, IRequireConsoleKeyInput, IDisposable
    {
        public event Action ViewClosed;

        private static readonly TimeSpan Millisecond = TimeSpan.FromMilliseconds(1);

        private readonly InclusiveValueRange<int> algorithmKeysValueRange;
        private readonly IMessenger messenger;
        private readonly ManualResetEventSlim resetEvent;
        private readonly ConsoleKeystrokesHook keystrokesHook;

        private InclusiveValueRange<TimeSpan> DelayRange => Constants.AlgorithmDelayTimeValueRange;

        public IInput<int> IntInput { get; set; }

        public IInput<Answer> AnswerInput { get; set; }

        public IInput<ConsoleKey> KeyInput { get; set; }

        public IInput<TimeSpan> TimeSpanInput { get; set; }

        public string AlgorithmKeyInputMessage { private get; set; }

        private string Statistics => Path.ToStatistics(timer, visitedVerticesCount, Algorithm.ToString());

        private int AlgorithmIndex => IntInput.Input(AlgorithmKeyInputMessage, algorithmKeysValueRange) - 1;

        public PathFindingViewModel(VisualPathfindingRange<Vertex> range, ICache<Graph2D<Vertex>> graph,
            IEnumerable<IAlgorithmFactory<PathfindingProcess>> algorithmFactories, ILog log)
            : base(range, algorithmFactories, graph.Cached, log)
        {
            algorithmKeysValueRange = new InclusiveValueRange<int>(Algorithms.Count, 1);
            keystrokesHook = DI.Container.Resolve<ConsoleKeystrokesHook>();
            keystrokesHook.KeyPressed += OnConsoleKeyPressed;
            Delay = DelayRange.LowerValueOfRange;
            resetEvent = new ManualResetEventSlim();
            messenger = DI.Container.Resolve<IMessenger>();
        }

        [Order(0)]
        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(CanExecutePathfinding))]
        [MenuItem(MenuItemsNames.FindPath)]
        public override void FindPath()
        {
            using (Cursor.HideCursor())
            {
                base.FindPath();
                resetEvent.Reset();
                resetEvent.Wait();
                KeyInput.Input();
            }
        }

        [Order(3)]
        [Condition(nameof(IsVisualizationNeeded))]
        [MenuItem(MenuItemsNames.InputDelayTime)]
        private void InputDelayTime()
        {
            Delay = TimeSpanInput.Input(MessagesTexts.DelayTimeInputMsg, DelayRange);
        }

        [Order(1)]
        [MenuItem(MenuItemsNames.ChooseAlgorithm)]
        private void ChooseAlgorithm()
        {
            Algorithm = Algorithms[AlgorithmIndex];
        }

        [Order(7)]
        [MenuItem(MenuItemsNames.Exit)]
        public void Interrupt()
        {
            ViewClosed?.Invoke();
        }

        [Order(2)]
        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.ChooseEndPoints)]
        public void ChooseExtremeVertex()
        {
            DI.Container.Display<PathfindingRangeView>();
        }

        [Order(4)]
        [MenuItem(MenuItemsNames.ClearGraph)]
        public void ClearGraph()
        {
            using (Cursor.HideCursor())
            {
                messenger.Send(new ClearGraphMessage());
            }
        }

        [Order(5)]
        [MenuItem(MenuItemsNames.ClearColors)]
        public void ClearColors()
        {
            messenger.Send(new ClearColorsMessage());
        }

        [Order(6)]
        [MenuItem(MenuItemsNames.ApplyVisualization)]
        public void ApplyVisualization()
        {
            IsVisualizationRequired = AnswerInput.Input(MessagesTexts.ApplyVisualizationMsg, Answer.Range);
        }

        public void Dispose()
        {
            ViewClosed = null;
            ClearGraph();
            keystrokesHook.KeyPressed -= OnConsoleKeyPressed;
            resetEvent.Dispose();
        }

        protected override void SummarizePathfindingResults()
        {
            string statistics = Path.Count == 0 ? MessagesTexts.CouldntFindPathMsg : Statistics;
            messenger.Send(new UpdateStatisticsMessage(statistics));
            visitedVerticesCount = 0;
            resetEvent.Set();
        }

        protected override void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            Delay.Wait();
            base.OnVertexVisited(sender, e);
            messenger.Send(new UpdateStatisticsMessage(Statistics));
        }

        protected override void SubscribeOnAlgorithmEvents(PathfindingProcess algorithm)
        {
            base.SubscribeOnAlgorithmEvents(algorithm);
            algorithm.Started += keystrokesHook.StartAsync;
            algorithm.Finished += (s, e) => keystrokesHook.Interrupt();
        }

        private void OnConsoleKeyPressed(object sender, ConsoleKeyPressedEventArgs e)
        {
            switch (e.PressedKey)
            {
                case ConsoleKey.Escape:
                    algorithm.Interrupt();
                    break;
                case ConsoleKey.P:
                    algorithm.Pause();
                    break;
                case ConsoleKey.Enter:
                    algorithm.Resume();
                    break;
                case ConsoleKey.DownArrow:
                    Delay = DelayRange.ReturnInRange(Delay - Millisecond);
                    break;
                case ConsoleKey.UpArrow:
                    Delay = DelayRange.ReturnInRange(Delay + Millisecond);
                    break;
            }
        }

        private bool IsVisualizationNeeded()
        {
            return IsVisualizationRequired;
        }

        private bool CanExecutePathfinding()
        {
            return !pathfindingRange.HasIsolators() && Algorithm is not null;
        }

        private void ExecuteSafe(Command action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}
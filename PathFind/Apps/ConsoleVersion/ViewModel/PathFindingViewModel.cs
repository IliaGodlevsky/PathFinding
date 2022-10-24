using Algorithm.Base;
using Algorithm.Factory.Interface;
using Algorithm.Infrastructure.EventArguments;
using Autofac;
using Common.Extensions;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.Delegates;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.EventArguments;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using ConsoleVersion.Model;
using ConsoleVersion.Views;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using GraphViewModel;
using Interruptable.Extensions;
using Logging.Interface;
using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.Threading;
using ValueRange;
using ValueRange.Extensions;

namespace ConsoleVersion.ViewModel
{
    internal sealed class PathFindingViewModel : PathFindingModel, IViewModel, IRequireTimeSpanInput, IRequireIntInput, IRequireAnswerInput, IRequireConsoleKeyInput, IDisposable
    {
        public event Action WindowClosed;

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

        private string Statistics => path.ToStatistics(timer, visitedVerticesCount, Algorithm.ToString());

        private int AlgorithmIndex => IntInput.Input(AlgorithmKeyInputMessage, algorithmKeysValueRange) - 1;

        public PathFindingViewModel(BasePathfindingRange endPoints, IEnumerable<IAlgorithmFactory<PathfindingAlgorithm>> algorithmFactories, ILog log)
            : base(endPoints, algorithmFactories, log)
        {
            algorithmKeysValueRange = new InclusiveValueRange<int>(Algorithms.Count, 1);
            keystrokesHook = DI.Container.Resolve<ConsoleKeystrokesHook>();
            keystrokesHook.KeyPressed += OnConsoleKeyPressed;
            Delay = DelayRange.LowerValueOfRange;
            resetEvent = new ManualResetEventSlim();
            messenger = DI.Container.Resolve<IMessenger>();
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(CanExecutePathfinding))]
        [MenuItem(MenuItemsNames.FindPath, 0)]
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

        [Condition(nameof(IsVisualizationNeeded))]
        [MenuItem(MenuItemsNames.InputDelayTime, 3)]
        public void InputDelayTime()
        {
            Delay = TimeSpanInput.Input(MessagesTexts.DelayTimeInputMsg, DelayRange);
        }

        [MenuItem(MenuItemsNames.ChooseAlgorithm, 1)]
        public void ChooseAlgorithm()
        {
            Algorithm = Algorithms[AlgorithmIndex];
        }

        [MenuItem(MenuItemsNames.Exit, 7)]
        public void Interrupt()
        {
            WindowClosed?.Invoke();
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.ChoosePathfindingRange, 2)]
        public void ChooseExtremeVertex()
        {
            DI.Container.Display<PathfindingRangeView>();
        }

        [MenuItem(MenuItemsNames.ClearGraph, 4)]
        public void ClearGraph()
        {
            using (Cursor.HideCursor())
            {
                messenger.Send(new ClearGraphMessage());
            }
        }

        [MenuItem(MenuItemsNames.ClearColors, 5)]
        public void ClearColors()
        {
            messenger.Send(new ClearColorsMessage());
        }

        [MenuItem(MenuItemsNames.ApplyVisualization, 6)]
        public void ApplyVisualization()
        {
            IsVisualizationRequired = AnswerInput.Input(MessagesTexts.ApplyVisualizationMsg, Answer.Range);
        }

        public void Dispose()
        {
            WindowClosed = null;
            ClearGraph();
            keystrokesHook.KeyPressed -= OnConsoleKeyPressed;
            resetEvent.Dispose();
        }

        protected override void SummarizePathfindingResults()
        {
            string statistics = path.IsNull() ? MessagesTexts.CouldntFindPathMsg : Statistics;
            messenger.Send(new UpdateStatisticsMessage(statistics));
            visitedVerticesCount = 0;
            resetEvent.Set();
        }

        protected override void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            Delay.Wait();
            base.OnVertexVisited(sender, e);
            messenger.Send(new UpdateStatisticsMessage(Statistics));
        }

        protected override void SubscribeOnAlgorithmEvents(PathfindingAlgorithm algorithm)
        {
            base.SubscribeOnAlgorithmEvents(algorithm);
            algorithm.Started += keystrokesHook.StartAsync;
            algorithm.Finished += keystrokesHook.Interrupt;
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
            return !pathfindingRange.HasIsolators() && !Algorithm.IsNull();
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
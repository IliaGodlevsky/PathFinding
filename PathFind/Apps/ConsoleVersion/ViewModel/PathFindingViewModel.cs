using Algorithm.Base;
using Algorithm.Factory.Interface;
using Algorithm.Infrastructure.EventArguments;
using Autofac;
using Commands.Extensions;
using Common;
using Common.Extensions;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Enums;
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
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using ValueRange;

namespace ConsoleVersion.ViewModel
{
    internal sealed class PathFindingViewModel : PathFindingModel,
        IViewModel, IRequireIntInput, IRequireAnswerInput, IRequireConsoleKeyInput, IDisposable
    {
        public event Action WindowClosed;

        private readonly InclusiveValueRange<int> algorithmKeysValueRange;
        private readonly IMessenger messenger;
        private readonly ManualResetEventSlim resetEvent;
        private readonly ValueTypeWrap<int> delayTime;

        private string currentAlgorithmName;

        public IInput<int> IntInput { get; set; }

        public IInput<Answer> AnswerInput { get; set; }

        public IInput<ConsoleKey> KeyInput { get; set; }

        public string AlgorithmKeyInputMessage { private get; set; }

        public override int DelayTime
        {
            get => delayTime.Value;
            set => delayTime.Value = value;
        }

        private string Statistics => path.ToStatistics(timer, visitedVerticesCount, currentAlgorithmName);

        private int AlgorithmIndex => IntInput.Input(AlgorithmKeyInputMessage, algorithmKeysValueRange) - 1;

        private IReadOnlyCollection<IConsoleKeyCommand<ValueTypeWrap<int>>> TimeDelayKeyCommands { get; }

        private IReadOnlyCollection<IConsoleKeyCommand<PathfindingAlgorithm>> AlgorithmKeyCommands { get; }

        public PathFindingViewModel(BaseEndPoints endPoints, IEnumerable<IAlgorithmFactory<PathfindingAlgorithm>> algorithmFactories,
            ILog log) : base(endPoints, algorithmFactories, log)
        {
            algorithmKeysValueRange = new InclusiveValueRange<int>(Algorithms.Count, 1);
            ConsoleKeystrokesHook.Instance.KeyPressed += OnConsoleKeyPressed;
            delayTime = new ValueTypeWrap<int>();
            DelayTime = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange;
            resetEvent = new ManualResetEventSlim();
            messenger = DI.Container.Resolve<IMessenger>();
            TimeDelayKeyCommands = this.GetAttachedDelayTimeConsoleKeyCommands();
            AlgorithmKeyCommands = this.GetAttachedPathfindingKeyCommands();
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [PreValidationMethod(nameof(CanExecutePathfinding))]
        [MenuItem(0), Description(MenuItemsNames.FindPath)]
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

        [PreValidationMethod(nameof(IsVisualizationNeeded))]
        [MenuItem(3), Description(MenuItemsNames.InputDelayTime)]
        public void InputDelayTime()
        {
            DelayTime = IntInput.Input(MessagesTexts.DelayTimeInputMsg, Constants.AlgorithmDelayTimeValueRange);
        }

        [MenuItem(1), Description(MenuItemsNames.ChooseAlgorithm)]
        public void ChooseAlgorithm()
        {
            Algorithm = Algorithms[AlgorithmIndex].Item2;
        }

        [MenuItem(7), Description(MenuItemsNames.Exit)]
        public void Interrupt()
        {
            WindowClosed?.Invoke();
        }

        [MenuItem(2), Description(MenuItemsNames.ChooseEndPoints)]
        public void ChooseExtremeVertex()
        {
            DI.Container.Display<EndPointsView>();
        }

        [MenuItem(4), Description(MenuItemsNames.ClearGraph)]
        public void ClearGraph()
        {
            using (Cursor.HideCursor())
            {
                messenger.Forward<ClearGraphMessage>(MessageTokens.MainModel);
            }
        }

        [MenuItem(5), Description(MenuItemsNames.ClearColors)]
        public void ClearColors()
        {
            messenger.Forward<ClearColorsMessage>(MessageTokens.MainModel);
        }

        [MenuItem(6), Description(MenuItemsNames.ApplyVisualization)]
        public void ApplyVisualization()
        {
            IsVisualizationRequired = AnswerInput.InputAnswer(MessagesTexts.ApplyVisualizationMsg, Constants.AnswerValueRange);
        }

        public void Dispose()
        {
            WindowClosed = null;
            ClearGraph();
            ConsoleKeystrokesHook.Instance.KeyPressed -= OnConsoleKeyPressed;
            resetEvent.Dispose();
        }

        protected override void SummarizePathfindingResults()
        {
            string statistics = path.IsNull() ? MessagesTexts.CouldntFindPathMsg : Statistics;
            var message = new UpdateStatisticsMessage(statistics);
            messenger.Forward(message, MessageTokens.MainView);
            visitedVerticesCount = 0;
            resetEvent.Set();
        }

        protected override void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            Stopwatch.StartNew().Wait(DelayTime).Stop();
            base.OnVertexVisited(sender, e);
            var message = new UpdateStatisticsMessage(Statistics);
            messenger.Forward(message, MessageTokens.MainView);
        }

        protected override void SubscribeOnAlgorithmEvents(PathfindingAlgorithm algorithm)
        {
            base.SubscribeOnAlgorithmEvents(algorithm);
            ConsoleKeystrokesHook.Instance.KeyInput = KeyInput;
            algorithm.Started += ConsoleKeystrokesHook.Instance.StartAsync;
            algorithm.Finished += ConsoleKeystrokesHook.Instance.Interrupt;
            currentAlgorithmName = Algorithm.GetDescription();
        }

        private void OnConsoleKeyPressed(object sender, ConsoleKeyPressedEventArgs e)
        {
            AlgorithmKeyCommands.ExecuteFirst(e.PressedKey, algorithm);
            TimeDelayKeyCommands.ExecuteFirst(e.PressedKey, delayTime);
        }

        private bool IsVisualizationNeeded()
        {
            return IsVisualizationRequired;
        }

        private bool CanExecutePathfinding()
        {
            return !endPoints.HasIsolators() && !Algorithm.IsNull();
        }

        private void ExecuteSafe(Action action)
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
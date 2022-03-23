using Algorithm.Base;
using Algorithm.Factory.Interface;
using Algorithm.Infrastructure.EventArguments;
using Autofac;
using Commands.Extensions;
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
using Interruptable.EventArguments;
using Interruptable.Extensions;
using Logging.Interface;
using NullObject.Extensions;
using System;
using System.Collections.Generic;
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

        public IInput<int> IntInput { get; set; }

        public IInput<Answer> AnswerInput { get; set; }

        public IInput<ConsoleKey> KeyInput { get; set; }

        public string AlgorithmKeyInputMessage { private get; set; }

        public PathfindingAlgorithm CurrentAlgorithm => algorithm;

        private string CurrentAlgorithmName { get; set; }

        private string Statistics => path.ToStatistics(timer, visitedVerticesCount, CurrentAlgorithmName);

        private int AlgorithmIndex => IntInput.Input(AlgorithmKeyInputMessage, algorithmKeysValueRange) - 1;

        private IReadOnlyCollection<IConsoleKeyCommand> KeyCommands { get; }

        public PathFindingViewModel(BaseEndPoints endPoints,
            IEnumerable<IAlgorithmFactory<PathfindingAlgorithm>> algorithmFactories, ILog log)
            : base(endPoints, algorithmFactories, log)
        {
            algorithmKeysValueRange = new InclusiveValueRange<int>(Algorithms.Count, 1);
            ConsoleKeystrokesHook.Instance.KeyPressed += OnConsoleKeyPressed;
            DelayTime = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange;
            resetEvent = new ManualResetEventSlim();
            messenger = DI.Container.Resolve<IMessenger>();
            KeyCommands = this.GetAttachedConsoleKeyCommands();
        }

        [MenuItem(MenuItemsNames.FindPath, MenuItemPriority.Highest)]
        public override void FindPath()
        {
            if (!endPoints.HasIsolators() && !Algorithm.IsNull())
            {
                try
                {
                    using (Cursor.HideCursor())
                    {
                        base.FindPath();
                        resetEvent.Reset();
                        resetEvent.Wait();
                        KeyInput.Input();
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            else
            {
                log.Warn(MessagesTexts.EndPointsFirstlyMsg);
            }
        }

        [MenuItem(MenuItemsNames.InputDelayTime, MenuItemPriority.Normal)]
        public void InputDelayTime()
        {
            if (IsVisualizationRequired)
            {
                DelayTime = IntInput.Input(MessagesTexts.DelayTimeInputMsg, Constants.AlgorithmDelayTimeValueRange);
            }
        }

        [MenuItem(MenuItemsNames.ChooseAlgorithm, MenuItemPriority.High)]
        public void ChooseAlgorithm()
        {
            Algorithm = Algorithms[AlgorithmIndex].Item2;
        }

        [MenuItem(MenuItemsNames.Exit, MenuItemPriority.Lowest)]
        public void Interrupt()
        {
            WindowClosed?.Invoke();
        }

        [MenuItem(MenuItemsNames.ChooseEndPoints, MenuItemPriority.High)]
        public void ChooseExtremeVertex()
        {
            DI.Container.Display<EndPointsView>();
        }

        [MenuItem(MenuItemsNames.ClearGraph, MenuItemPriority.Low)]
        public void ClearGraph()
        {
            using (Cursor.HideCursor())
            {
                messenger.Forward<ClearGraphMessage>(MessageTokens.MainModel);
            }
        }

        [MenuItem(MenuItemsNames.ClearColors, MenuItemPriority.Low)]
        public void ClearColors()
        {
            messenger.Forward<ClearColorsMessage>(MessageTokens.MainModel);
        }

        [MenuItem(MenuItemsNames.ApplyVisualization, MenuItemPriority.Low)]
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

        protected override void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            CurrentAlgorithmName = Algorithm.GetDescription();
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
        }

        private void OnConsoleKeyPressed(object sender, ConsoleKeyPressedEventArgs e)
        {
            KeyCommands.ExecuteFirst(e.PressedKey, this);
        }
    }
}
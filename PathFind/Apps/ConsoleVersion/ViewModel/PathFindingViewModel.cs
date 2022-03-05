using Algorithm.Base;
using Algorithm.Factory;
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
        IViewModel, IRequireIntInput, IRequireAnswerInput, IDisposable
    {
        public event Action WindowClosed;

        public string AlgorithmKeyInputMessage { private get; set; }

        public IValueInput<int> IntInput { get; set; }
        public IValueInput<Answer> AnswerInput { get; set; }
        public PathfindingAlgorithm CurrentAlgorithm => algorithm;
        private IReadOnlyCollection<IConsoleKeyCommand> KeyCommands { get; }
        private string Statistics => path.ToStatistics(timer, visitedVerticesCount, CurrentAlgorithmName);
        private string CurrentAlgorithmName { get; set; }

        public PathFindingViewModel(BaseEndPoints endPoints, IEnumerable<IAlgorithmFactory> algorithmFactories, ILog log)
            : base(endPoints, algorithmFactories, log)
        {
            algorithmKeysValueRange = new InclusiveValueRange<int>(Algorithms.Length, 1);
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
                    base.FindPath();
                    resetEvent.Wait();
                    Console.ReadKey(true);
                    Console.CursorVisible = true;
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

        [MenuItem(MenuItemsNames.ChooseAlgorithm, MenuItemPriority.High)]
        public void ChooseAlgorithm()
        {
            int algorithmKeyIndex = IntInput.InputValue(AlgorithmKeyInputMessage, algorithmKeysValueRange) - 1;
            Algorithm = Algorithms[algorithmKeyIndex].Item2;
        }

        [MenuItem(MenuItemsNames.InputDelayTime, MenuItemPriority.Normal)]
        public void InputDelayTime()
        {
            if (IsVisualizationRequired)
            {
                DelayTime = IntInput.InputValue(MessagesTexts.DelayTimeInputMsg, Constants.AlgorithmDelayTimeValueRange);
            }
        }

        [MenuItem(MenuItemsNames.Exit, MenuItemPriority.Lowest)]
        public void Interrupt()
        {
            WindowClosed?.Invoke();
        }

        [MenuItem(MenuItemsNames.ChooseEndPoints, MenuItemPriority.High)]
        public void ChooseExtremeVertex() => DI.Container.Display<EndPointsView>();

        [MenuItem(MenuItemsNames.ClearGraph, MenuItemPriority.Low)]
        public void ClearGraph() => messenger.Forward<ClearGraphMessage>(MessageTokens.MainModel);

        [MenuItem(MenuItemsNames.ClearColors, MenuItemPriority.Low)]
        public void ClearColors() => messenger.Forward<ClearColorsMessage>(MessageTokens.MainModel);

        [MenuItem(MenuItemsNames.ApplyVisualization, MenuItemPriority.Low)]
        public void ApplyVisualization()
        {
            var answer = AnswerInput.InputValue(MessagesTexts.ApplyVisualizationMsg, Constants.AnswerValueRange);
            IsVisualizationRequired = answer == Answer.Yes;
        }

        protected override void SummarizePathfindingResults()
        {
            var message = new UpdateStatisticsMessage(Statistics);
            messenger.Forward(message, MessageTokens.MainView);
            visitedVerticesCount = 0;
            resetEvent.Set();
        }

        protected override void OnAlgorithmInterrupted(object sender, ProcessEventArgs e) { }
        protected override void OnAlgorithmFinished(object sender, ProcessEventArgs e) { }

        protected override void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            resetEvent.Reset();
            Console.CursorVisible = false;
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
            algorithm.Started += ConsoleKeystrokesHook.Instance.StartAsync;
            algorithm.Finished += ConsoleKeystrokesHook.Instance.Interrupt;
        }

        private void OnConsoleKeyPressed(object sender, ConsoleKeyPressedEventArgs e)
        {
            KeyCommands.Execute(e.PressedKey, this);
        }

        public void Dispose()
        {
            WindowClosed = null;
            ClearGraph();
            ConsoleKeystrokesHook.Instance.KeyPressed -= OnConsoleKeyPressed;
            resetEvent.Dispose();
        }

        private readonly InclusiveValueRange<int> algorithmKeysValueRange;
        private readonly IMessenger messenger;
        private readonly ManualResetEventSlim resetEvent;
    }
}
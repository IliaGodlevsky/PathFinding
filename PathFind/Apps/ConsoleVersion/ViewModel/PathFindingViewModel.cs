using Algorithm.Base;
using Algorithm.Factory;
using Algorithm.Infrastructure.EventArguments;
using Autofac;
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
using ConsoleVersion.ValueInput;
using ConsoleVersion.Views;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphViewModel;
using Logging.Interface;
using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using ValueRange;
using ValueRange.Enums;
using ValueRange.Extensions;

namespace ConsoleVersion.ViewModel
{
    internal sealed class PathFindingViewModel : PathFindingModel,
        IViewModel, IRequireInt32Input, IRequireAnswerInput, IDisposable
    {
        public event Action WindowClosed;

        public string AlgorithmKeyInputMessage { private get; set; }

        public ConsoleValueInput<int> Int32Input { get; set; }
        public ConsoleValueInput<Answer> AnswerInput { get; set; }

        public PathFindingViewModel(BaseEndPoints endPoints, IEnumerable<IAlgorithmFactory> algorithmFactories, ILog log)
            : base(endPoints, algorithmFactories, log)
        {
            algorithmKeysValueRange = new InclusiveValueRange<int>(Algorithms.Length, 1);
            ConsoleKeystrokesHook.Instance.KeyPressed += OnConsoleKeyPressed;
            DelayTime = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange;
            messenger = DI.Container.Resolve<IMessenger>();
        }

        [MenuItem(MenuItemsNames.FindPath, MenuItemPriority.Highest)]
        public override void FindPath()
        {
            if (!endPoints.HasIsolators() && !Algorithm.IsNull())
            {
                try
                {
                    Console.CursorVisible = false;
                    CurrentAlgorithmName = Algorithm.GetDescriptionAttributeValueOrEmpty();
                    base.FindPath();
                    ConsoleKeystrokesHook.Instance.StartHookingConsoleKeystrokes();
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
            int algorithmKeyIndex = Int32Input.InputValue(AlgorithmKeyInputMessage, algorithmKeysValueRange) - 1;
            Algorithm = Algorithms[algorithmKeyIndex].Item2;
        }

        [MenuItem(MenuItemsNames.InputDelayTime, MenuItemPriority.Normal)]
        public void InputDelayTime()
        {
            if (IsVisualizationRequired)
            {
                DelayTime = Int32Input.InputValue(MessagesTexts.DelayTimeInputMsg, Constants.AlgorithmDelayTimeValueRange);
            }
        }

        [MenuItem(MenuItemsNames.Exit, MenuItemPriority.Lowest)]
        public void Interrupt()
        {
            WindowClosed?.Invoke();
        }

        [MenuItem(MenuItemsNames.ChooseEndPoints, MenuItemPriority.High)]
        public void ChooseExtremeVertex()
        {
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                var view = scope.Resolve<EndPointsView>();
                view.Start();
            }
        }

        [MenuItem(MenuItemsNames.ClearGraph, MenuItemPriority.Low)]
        public void ClearGraph()
        {
            messenger.Forward(new ClearGraphMessage(), MessageTokens.MainModel);
        }

        [MenuItem(MenuItemsNames.ClearColors, MenuItemPriority.Low)]
        public void ClearColors()
        {
            messenger.Forward(new ClearColorsMessage(), MessageTokens.MainModel);
        }

        [MenuItem(MenuItemsNames.ApplyVisualization, MenuItemPriority.Low)]
        public void ApplyVisualization()
        {
            var answer = AnswerInput.InputValue(MessagesTexts.ApplyVisualizationMsg, Constants.AnswerValueRange);
            IsVisualizationRequired = answer == Answer.Yes;
        }

        protected override void SummarizePathfindingResults()
        {
            string statistics = !path.IsNull() ? GetStatistics() : MessagesTexts.CouldntFindPathMsg;
            messenger.Forward(new UpdateStatisticsMessage(statistics), MessageTokens.MainView);
            visitedVerticesCount = 0;
        }

        protected override void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            Stopwatch.StartNew().Wait(DelayTime).Stop();
            base.OnVertexVisited(sender, e);
            messenger.Forward(new UpdateStatisticsMessage(GetStatistics()), MessageTokens.MainView);
        }

        protected override void SubscribeOnAlgorithmEvents(PathfindingAlgorithm algorithm)
        {
            base.SubscribeOnAlgorithmEvents(algorithm);
            algorithm.Finished += ConsoleKeystrokesHook.Instance.CancelHookingConsoleKeystrokes;
        }

        private string GetStatistics()
        {
            string timerInfo = timer.ToFormattedString();
            var pathfindingInfos = new object[] { path.Length, path.Cost, visitedVerticesCount };
            string pathfindingInfo = string.Format(MessagesTexts.PathfindingStatisticsFormat, pathfindingInfos);
            return string.Join("\t", CurrentAlgorithmName, timerInfo, pathfindingInfo);
        }

        private void OnConsoleKeyPressed(object sender, ConsoleKeyPressedEventArgs e)
        {
            switch (e.PressedKey)
            {
                case ConsoleKey.Escape:
                    algorithm.Interrupt();
                    break;
                case ConsoleKey.UpArrow:
                    DelayTime = Constants.AlgorithmDelayTimeValueRange.ReturnInRange(DelayTime - 1, ReturnOptions.Cycle);
                    break;
                case ConsoleKey.DownArrow:
                    DelayTime = Constants.AlgorithmDelayTimeValueRange.ReturnInRange(DelayTime + 1, ReturnOptions.Cycle);
                    break;
            }
        }

        public void Dispose()
        {
            WindowClosed = null;
            ClearGraph();
            ConsoleKeystrokesHook.Instance.KeyPressed -= OnConsoleKeyPressed;
        }

        private string CurrentAlgorithmName { get; set; }

        private readonly InclusiveValueRange<int> algorithmKeysValueRange;
        private readonly IMessenger messenger;
    }
}
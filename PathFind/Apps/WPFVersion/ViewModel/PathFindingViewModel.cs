using Algorithm.Base;
using Algorithm.Factory;
using Algorithm.Infrastructure.EventArguments;
using Autofac;
using Common.Extensions;
using Common.Interface;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphViewModel;
using Interruptable.EventArguments;
using Logging.Interface;
using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using WPFVersion.DependencyInjection;
using WPFVersion.Enums;
using WPFVersion.Extensions;
using WPFVersion.Infrastructure;
using WPFVersion.Messages;

namespace WPFVersion.ViewModel
{
    public class PathFindingViewModel : PathFindingModel, IViewModel, IDisposable
    {
        public event Action WindowClosed;

        public ICommand ConfirmPathFindAlgorithmChoice { get; }
        public ICommand CancelPathFindAlgorithmChoice { get; }

        public PathFindingViewModel(BaseEndPoints endPoints, IEnumerable<IAlgorithmFactory> algorithmFactories, ILog log)
            : base(endPoints, algorithmFactories, log)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            ConfirmPathFindAlgorithmChoice = new RelayCommand(ExecuteConfirmPathFindAlgorithmChoice,
                CanExecuteConfirmPathFindAlgorithmChoice);
            CancelPathFindAlgorithmChoice = new RelayCommand(ExecuteCloseWindowCommand);
            messenger.Register<AlgorithmIndexMessage>(this, MessageTokens.PathfindingModel, SetAlgorithmIndex);
            messenger.Register<DelayTimeChangedMessage>(this, MessageTokens.PathfindingModel, SetAlgorithmDelayTime);
            DelayTime = Convert.ToInt32(Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange);
        }

        private void SetAlgorithmIndex(AlgorithmIndexMessage message)
        {
            messenger.Unregister<AlgorithmIndexMessage>(this, MessageTokens.PathfindingModel, SetAlgorithmIndex);
            Index = message.Index;
        }

        private void SetAlgorithmDelayTime(DelayTimeChangedMessage message)
        {
            if (Index == message.Index)
            {
                DelayTime = message.DelayTime;
            }
        }

        protected override void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmStarted(sender, e);
            messenger
                .Forward(new AlgorithmStartedMessage(algorithm, DelayTime), MessageTokens.AlgorithmStatisticsModel)
                .Forward(new EndPointsChosenMessage(algorithm, endPoints), MessageTokens.VisualizationModel);
        }

        protected override void SummarizePathfindingResults()
        {
            string time = timer.ToFormattedString();
            var updateMessage = new UpdateStatisticsMessage(Index, time, visitedVerticesCount, path.Length, path.Cost);
            messenger
                .Forward(updateMessage, MessageTokens.AlgorithmStatisticsModel)
                .Forward(new AlgorithmStatusMessage(path.ToStatus(), Index), MessageTokens.AlgorithmStatisticsModel)
                .Forward(new PathFoundMessage(algorithm, path), MessageTokens.VisualizationModel);
        }

        protected override async void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            Stopwatch.StartNew().Wait(DelayTime).Stop();
            var message = new UpdateStatisticsMessage(Index, timer.ToFormattedString(), visitedVerticesCount);
            await messenger.ForwardAsync(message, MessageTokens.AlgorithmStatisticsModel);
            if (!e.Current.IsNull())
            {
                visitedVerticesCount++;
            }
        }

        protected override void OnVertexEnqueued(object sender, AlgorithmEventArgs e) { }

        protected override void OnAlgorithmInterrupted(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmInterrupted(sender, e);
            var message = new AlgorithmStatusMessage(AlgorithmStatus.Interrupted, Index);
            messenger.Forward(message, MessageTokens.AlgorithmStatisticsModel);
        }

        protected override void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmFinished(sender, e);
            messenger.Unregister(this);
        }

        private void ExecuteCloseWindowCommand(object param)
        {
            WindowClosed?.Invoke();
        }

        private void ExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            ExecuteCloseWindowCommand(null);
            base.FindPath();
        }

        protected override void SubscribeOnAlgorithmEvents(PathfindingAlgorithm algorithm)
        {
            var message = new SubscribeOnAlgorithmEventsMessage(algorithm, IsVisualizationRequired);
            messenger.Forward(message, MessageTokens.VisualizationModel);
            base.SubscribeOnAlgorithmEvents(algorithm);
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            return Algorithm != null;
        }

        public void Dispose()
        {
            WindowClosed = null;
        }

        private int Index { get; set; }

        private readonly IMessenger messenger;
    }
}
using Algorithm.Base;
using Algorithm.Factory.Interface;
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
using System.Windows.Input;
using WPFVersion.DependencyInjection;
using WPFVersion.Extensions;
using WPFVersion.Infrastructure;
using WPFVersion.Messages;
using WPFVersion.Messages.DataMessages;

namespace WPFVersion.ViewModel
{
    public class PathFindingViewModel : PathFindingModel, IViewModel, IDisposable
    {
        public event Action WindowClosed;

        public ICommand ConfirmPathFindAlgorithmChoice { get; }
        public ICommand CancelPathFindAlgorithmChoice { get; }

        public PathFindingViewModel(BaseEndPoints endPoints,
            IEnumerable<IAlgorithmFactory<PathfindingAlgorithm>> algorithmFactories, ILog log)
            : base(endPoints, algorithmFactories, log)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            ConfirmPathFindAlgorithmChoice = new RelayCommand(ExecuteConfirmPathFindAlgorithmChoice, CanExecuteConfirmPathFindAlgorithmChoice);
            CancelPathFindAlgorithmChoice = new RelayCommand(ExecuteCloseWindowCommand);
            messenger.Register<AlgorithmIndexMessage>(this, SetAlgorithmIndex);
            messenger.Register<DelayTimeChangedMessage>(this, SetAlgorithmDelayTime);
            DelayTime = Convert.ToInt32(Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange);
        }

        private void SetAlgorithmIndex(AlgorithmIndexMessage message)
        {
            messenger.Unregister<AlgorithmIndexMessage>(this, SetAlgorithmIndex);
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
            messenger.Send(new AlgorithmStartedMessage(algorithm, DelayTime));
            messenger.Send(new EndPointsChosenMessage(algorithm, endPoints));
        }

        protected override void SummarizePathfindingResults()
        {
            string time = timer.Elapsed.ToString(@"mm\:ss\.fff");
            var updateMessage = new UpdateStatisticsMessage(Index, time, visitedVerticesCount, path.Length, path.Cost);
            messenger.Send(updateMessage);
            messenger.Send(new AlgorithmStatusMessage(path.ToStatus(), Index));
            messenger.Send(new PathFoundMessage(algorithm, path));
        }

        protected override async void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            TimeSpan.FromMilliseconds(DelayTime).Wait();
            var message = new UpdateStatisticsMessage(Index, timer.Elapsed.ToString(@"mm\:ss\.fff"), visitedVerticesCount);
            await messenger.SendAsync(message).ConfigureAwait(false);
            if (!e.Current.IsNull())
            {
                visitedVerticesCount++;
            }
        }

        protected override void OnVertexEnqueued(object sender, AlgorithmEventArgs e) { }

        protected override void OnAlgorithmInterrupted(object sender, ProcessEventArgs e)
        {
            var message = new AlgorithmStatusMessage(AlgorithmViewModel.Interrupted, Index);
            messenger.Send(message);
        }

        protected void OnAlgorithmPaused(object sender, ProcessEventArgs e)
        {
            var message = new AlgorithmStatusMessage(AlgorithmViewModel.Paused, Index);
            messenger.Send(message);
        }

        protected void OnAlgorithmUnpaused(object sender, ProcessEventArgs e)
        {
            var message = new AlgorithmStatusMessage(AlgorithmViewModel.Started, Index);
            messenger.Send(message);
        }

        protected override void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
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
            messenger.Send(message);
            algorithm.Paused += OnAlgorithmPaused;
            algorithm.Resumed += OnAlgorithmUnpaused;
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
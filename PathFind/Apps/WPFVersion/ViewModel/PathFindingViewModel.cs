using Algorithm.Base;
using Algorithm.Factory.Interface;
using Algorithm.Infrastructure.EventArguments;
using Autofac;
using Common.Extensions;
using Common.Interface;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Realizations.Graphs;
using GraphViewModel;
using Interruptable.EventArguments;
using Pathfinding.Logging.Interface;
using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using WPFVersion.DependencyInjection;
using WPFVersion.Extensions;
using WPFVersion.Infrastructure;
using WPFVersion.Interface;
using WPFVersion.Messages;
using WPFVersion.Messages.DataMessages;
using WPFVersion.Model;

namespace WPFVersion.ViewModel
{
    public class PathFindingViewModel : PathFindingModel<Vertex>, IViewModel, IDisposable
    {
        public event Action WindowClosed;

        private readonly IMessenger messenger;

        private int Index { get; set; }

        public ICommand ConfirmPathFindAlgorithmChoice { get; }

        public ICommand CancelPathFindAlgorithmChoice { get; }

        public PathFindingViewModel(BaseEndPoints<Vertex> endPoints,
            IEnumerable<IAlgorithmFactory<PathfindingProcess>> algorithmFactories, ICache<Graph2D<Vertex>> graphCache, ILog log)
            : base(endPoints, algorithmFactories, graphCache.Cached, log)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            ConfirmPathFindAlgorithmChoice = new RelayCommand(ExecuteConfirmPathFindAlgorithmChoice, CanExecuteConfirmPathFindAlgorithmChoice);
            CancelPathFindAlgorithmChoice = new RelayCommand(ExecuteCloseWindowCommand);
            messenger.Register<AlgorithmIndexMessage>(this, SetAlgorithmIndex);
            messenger.Register<DelayTimeChangedMessage>(this, SetAlgorithmDelayTime);
            Delay = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange;
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
                Delay = message.DelayTime;
            }
        }

        protected override void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            messenger.Send(new AlgorithmStartedMessage(algorithm, Delay));
            messenger.Send(new EndPointsChosenMessage(algorithm, endPoints));
        }

        protected override void SummarizePathfindingResults()
        {
            string time = timer.Elapsed.ToString(@"mm\:ss\.fff");
            var updateMessage = new UpdateStatisticsMessage(Index, time, visitedVerticesCount, Path.Count, Path.Cost);
            messenger.Send(updateMessage);
            messenger.Send(new AlgorithmStatusMessage(Path.ToStatus(), Index));
            messenger.Send(new PathFoundMessage(algorithm, Path));
        }

        protected override async void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            Delay.Wait();
            string time = timer.Elapsed.ToString(@"mm\:ss\.fff");
            var message = new UpdateStatisticsMessage(Index, time, visitedVerticesCount);
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

        protected override void SubscribeOnAlgorithmEvents(PathfindingProcess algorithm)
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
    }
}
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
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Extensions;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Messages.PassValueMessages;

namespace WPFVersion3D.ViewModel
{
    public class PathFindingViewModel : PathFindingModel, IViewModel, IDisposable
    {
        public event Action WindowClosed;

        private readonly IMessenger messenger;

        public ICommand FindPathCommand { get; }

        public ICommand CancelFindPathCommand { get; }

        private int Index { get; set; }

        public PathFindingViewModel(BaseEndPoints endPoints, IEnumerable<IAlgorithmFactory<PathfindingAlgorithm>> algorithmFactories,
            ILog log) : base(endPoints, algorithmFactories, log)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            FindPathCommand = new RelayCommand(ExecutePathFindCommand, CanExecutePathFindCommand);
            CancelFindPathCommand = new RelayCommand(ExecuteCloseWindowCommand);
            messenger.Register<AlgorithmIndexMessage>(this, SetAlgorithmIndex);
            Delay = Constants.AlgorithmDelayValueRange.LowerValueOfRange;
        }

        private void SetAlgorithmIndex(AlgorithmIndexMessage message)
        {
            messenger.Unregister<AlgorithmIndexMessage>(this, SetAlgorithmIndex);
            Index = message.Value;
        }

        protected override void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            messenger.Send(new AlgorithmStartedMessage(algorithm));
            messenger.Send(new EndPointsChosenMessage(endPoints, algorithm));
        }

        protected override void SummarizePathfindingResults()
        {
            var status = !path.IsNull() ? AlgorithmViewModel.Finished : AlgorithmViewModel.Failed;
            string time = timer.Elapsed.ToString(@"mm\:ss\.fff");
            messenger.Send(new UpdateAlgorithmStatisticsMessage(Index, time, visitedVerticesCount, path.Count, path.Cost));
            messenger.Send(new AlgorithmStatusMessage(status, Index));
            messenger.Send(new PathFoundMessage(path, algorithm));
        }

        protected override async void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            Delay.Wait();
            string time = timer.Elapsed.ToString(@"mm\:ss\.fff");
            var message = new UpdateAlgorithmStatisticsMessage(Index, time, visitedVerticesCount);
            await messenger.SendAsync(message).ConfigureAwait(false);
            if (!e.Current.IsNull())
            {
                visitedVerticesCount++;
            }
        }

        protected override void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
        }

        protected override void OnAlgorithmInterrupted(object sender, ProcessEventArgs e)
        {
            messenger.Send(AlgorithmStatusMessage.Interrupted(Index));
        }

        protected override void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            messenger.Unregister(this);
        }

        protected override void SubscribeOnAlgorithmEvents(PathfindingAlgorithm algorithm)
        {
            messenger.Send(new SubscribeOnAlgorithmEventsMessage(algorithm, IsVisualizationRequired));
            algorithm.Paused += OnAlgorithmPaused;
            algorithm.Resumed += OnAlgorithmUnpaused;
            base.SubscribeOnAlgorithmEvents(algorithm);
        }

        private void OnAlgorithmPaused(object sender, ProcessEventArgs e)
        {
            messenger.Send(AlgorithmStatusMessage.Paused(Index));
        }

        private void OnAlgorithmUnpaused(object sender, ProcessEventArgs e)
        {
            messenger.Send(AlgorithmStatusMessage.Started(Index));
        }

        private void ExecuteCloseWindowCommand(object param)
        {
            WindowClosed?.Invoke();
        }

        private void ExecutePathFindCommand(object param)
        {
            ExecuteCloseWindowCommand(null);
            base.FindPath();
        }

        private bool CanExecutePathFindCommand(object param)
        {
            return Algorithm != null;
        }

        public void Dispose()
        {
            WindowClosed = null;
        }
    }
}
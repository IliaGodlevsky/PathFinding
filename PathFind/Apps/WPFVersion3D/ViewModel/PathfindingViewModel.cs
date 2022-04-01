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
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Enums;
using WPFVersion3D.Extensions;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Messages;

namespace WPFVersion3D.ViewModel
{
    public class PathFindingViewModel : PathFindingModel, IViewModel, INotifyPropertyChanged, IDisposable
    {
        public event Action WindowClosed;
        public event PropertyChangedEventHandler PropertyChanged;

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
            messenger.Register<AlgorithmIndexMessage>(this, MessageTokens.PathfindingModel, SetAlgorithmIndex);
            DelayTime = Convert.ToInt32(Constants.AlgorithmDelayValueRange.LowerValueOfRange);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetAlgorithmIndex(AlgorithmIndexMessage message)
        {
            messenger.Unregister<AlgorithmIndexMessage>(this, MessageTokens.PathfindingModel, SetAlgorithmIndex);
            Index = message.Index;
        }

        protected override void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            var message = new AlgorithmStartedMessage(algorithm);
            messenger.Forward(message, MessageTokens.AlgorithmStatisticsModel);
        }

        protected override void SummarizePathfindingResults()
        {
            var status = !path.IsNull() ? AlgorithmStatuses.Finished : AlgorithmStatuses.Failed;
            string time = timer.ToFormattedString();
            var message = new UpdateAlgorithmStatisticsMessage(Index, time,
                visitedVerticesCount, path.Length, path.Cost);
            messenger.Forward(message, MessageTokens.AlgorithmStatisticsModel);
            var statusMessage = new AlgorithmStatusMessage(status, Index);
            messenger.Forward(statusMessage, MessageTokens.AlgorithmStatisticsModel);
        }

        protected override async void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            Stopwatch.StartNew().Wait(DelayTime).Stop();
            string time = timer.ToFormattedString();
            var message = new UpdateAlgorithmStatisticsMessage(Index, time, visitedVerticesCount);
            await messenger.ForwardAsync(message, MessageTokens.AlgorithmStatisticsModel).ConfigureAwait(false);
            await Task.Run(() => base.OnVertexVisited(sender, e)).ConfigureAwait(false);
        }

        protected override async void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            await Task.Run(() => base.OnVertexEnqueued(sender, e)).ConfigureAwait(false);
        }

        protected override void OnAlgorithmInterrupted(object sender, ProcessEventArgs e)
        {
            var message = new AlgorithmStatusMessage(AlgorithmStatuses.Interrupted, Index);
            messenger.Forward(message, MessageTokens.AlgorithmStatisticsModel);
        }

        protected override void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            messenger.Unregister(this);
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
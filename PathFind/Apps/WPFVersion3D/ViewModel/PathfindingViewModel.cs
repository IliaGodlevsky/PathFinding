using Algorithm.Factory;
using Algorithm.Infrastructure.EventArguments;
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand ConfirmPathFindAlgorithmChoice { get; }
        public ICommand CancelPathFindAlgorithmChoice { get; }

        public PathFindingViewModel(BaseEndPoints endPoints, IEnumerable<IAlgorithmFactory> algorithmFactories, ILog log)
            : base(endPoints, algorithmFactories, log)
        {
            ConfirmPathFindAlgorithmChoice = new RelayCommand(
                ExecuteConfirmPathFindAlgorithmChoice,
                CanExecuteConfirmPathFindAlgorithmChoice);
            CancelPathFindAlgorithmChoice = new RelayCommand(ExecuteCloseWindowCommand);
            Messenger.Default.Register<AlgorithmIndexMessage>(this, MessageTokens.PathfindingModel, SetAlgorithmIndex);
            DelayTime = Convert.ToInt32(Constants.AlgorithmDelayValueRange.LowerValueOfRange);
        }

        private void SetAlgorithmIndex(AlgorithmIndexMessage message)
        {
            Messenger.Default.Unregister<AlgorithmIndexMessage>(this, MessageTokens.PathfindingModel, SetAlgorithmIndex);
            Index = message.Index;
        }

        protected override void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmStarted(sender, e);
            string algorithmName = Algorithm.GetDescriptionAttributeValueOrEmpty();
            var message = new AlgorithmStartedMessage(algorithm, algorithmName);
            Messenger.Default.Forward(message, MessageTokens.AlgorithmStatisticsModel);
        }

        protected override void SummarizePathfindingResults()
        {
            var status = !path.IsNull() ? AlgorithmStatuses.Finished : AlgorithmStatuses.Failed;
            string time = timer.ToFormattedString();
            var message = new UpdateAlgorithmStatisticsMessage(Index, time,
                visitedVerticesCount, path.Length, path.Cost);
            Messenger.Default.Forward(message, MessageTokens.AlgorithmStatisticsModel);
            var statusMessage = new AlgorithmStatusMessage(status, Index);
            Messenger.Default.Forward(statusMessage, MessageTokens.AlgorithmStatisticsModel);
        }

        protected override async void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            Stopwatch.StartNew().Wait(DelayTime).Stop();
            string time = timer.ToFormattedString();
            var message = new UpdateAlgorithmStatisticsMessage(Index, time, visitedVerticesCount);
            await Messenger.Default.ForwardAsync(message, MessageTokens.AlgorithmStatisticsModel);
            await Task.Run(() => base.OnVertexVisited(sender, e));
        }

        protected override async void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            await Task.Run(() => base.OnVertexEnqueued(sender, e));
        }

        protected override void OnAlgorithmInterrupted(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmInterrupted(sender, e);
            var message = new AlgorithmStatusMessage(AlgorithmStatuses.Interrupted, Index);
            Messenger.Default.Forward(message, MessageTokens.AlgorithmStatisticsModel);
        }

        protected override void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmFinished(sender, e);
            Messenger.Default.Unregister(this);
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

        private bool CanExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            return Algorithm != null;
        }

        public void Dispose()
        {
            WindowClosed = null;
        }

        private int Index { get; set; }
    }
}

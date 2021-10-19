using Algorithm.Factory;
using Algorithm.Infrastructure.EventArguments;
using Common.Extensions;
using Common.Interface;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphViewModel;
using Interruptable.EventArguments;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFVersion3D.Enums;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Messages;

namespace WPFVersion3D.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel, IViewModel, INotifyPropertyChanged
    {
        public event EventHandler WindowClosed;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand ConfirmPathFindAlgorithmChoice { get; }
        public ICommand CancelPathFindAlgorithmChoice { get; }

        public PathFindingViewModel(ILog log, IGraph graph,
            BaseEndPoints endPoints, IEnumerable<IAlgorithmFactory> algorithmFactories)
            : base(log, graph, endPoints, algorithmFactories)
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
            string algorithmName = Algorithm.GetDescriptionAttributeValueOrTypeName();
            var message = new AlgorithmStartedMessage(algorithm, algorithmName);
            Messenger.Default.Send(message, MessageTokens.AlgorithmStatisticsModel);
        }

        protected override void SummarizePathfindingResults()
        {
            var status = path.PathLength > 0 ? AlgorithmStatuses.Finished : AlgorithmStatuses.Failed;
            string time = timer.ToString();
            var message = new UpdateAlgorithmStatisticsMessage(Index, time,
                visitedVerticesCount, path.PathLength, path.PathCost);
            Messenger.Default.Send(message, MessageTokens.AlgorithmStatisticsModel);
        }

        protected override async void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            Stopwatch.StartNew().Pause(DelayTime).Cancel();
            string time = timer.ToString();
            var message = new UpdateAlgorithmStatisticsMessage(Index, time, visitedVerticesCount);
            Messenger.Default.Send(message, MessageTokens.AlgorithmStatisticsModel);
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
            Messenger.Default.Send(message, MessageTokens.AlgorithmStatisticsModel);
        }

        protected override void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmFinished(sender, e);
            var message = new AlgorithmStatusMessage(AlgorithmStatuses.Finished, Index);
            Messenger.Default.Send(message, MessageTokens.AlgorithmStatisticsModel);
        }

        private void ExecuteCloseWindowCommand(object param)
        {
            WindowClosed?.Invoke(this, EventArgs.Empty);
            WindowClosed = null;
        }

        private void ExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            ExecuteCloseWindowCommand(null);
            base.FindPath();
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            return Algorithms.Any(item => item.Item2 == Algorithm);
        }

        private int Index { get; set; }
    }
}

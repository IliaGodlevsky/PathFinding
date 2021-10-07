using Algorithm.Infrastructure.EventArguments;
using Common.Extensions;
using Common.Interface;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Logging.Interface;
using NullObject.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFVersion.Enums;
using WPFVersion.Infrastructure;
using WPFVersion.Messages;

namespace WPFVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel, IModel, IViewModel
    {
        public event EventHandler WindowClosed;

        public ICommand ConfirmPathFindAlgorithmChoice { get; }
        public ICommand CancelPathFindAlgorithmChoice { get; }

        public PathFindingViewModel(ILog log, IMainModel model, BaseEndPoints endPoints)
            : base(log, model, endPoints)
        {
            ConfirmPathFindAlgorithmChoice = new RelayCommand(ExecuteConfirmPathFindAlgorithmChoice,
                CanExecuteConfirmPathFindAlgorithmChoice);
            CancelPathFindAlgorithmChoice = new RelayCommand(ExecuteCloseWindowCommand);
            Messenger.Default.Register<AlgorithmStatisticsIndexMessage>(this, MessageTokens.PathfindingModel, SetAlgorithmIndex);
            Messenger.Default.Register<DelayTimeChangedMessage>(this, MessageTokens.PathfindingModel, SetAlgorithmDelayTime);
        }

        private void SetAlgorithmIndex(AlgorithmStatisticsIndexMessage message)
        {
            Messenger.Default.Unregister<AlgorithmStatisticsIndexMessage>(this, MessageTokens.PathfindingModel, SetAlgorithmIndex);
            AlgorithmStatisticsIndex = message.Index;
        }

        private void SetAlgorithmDelayTime(DelayTimeChangedMessage message)
        {
            if (AlgorithmStatisticsIndex == message.Index)
            {
                DelayTime = message.DelayTime;
            }
        }

        protected override void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmStarted(sender, e);
            string algorithmName = Algorithm.GetDescriptionAttributeValueOrTypeName();
            var message = new AlgorithmStartedMessage(algorithm, algorithmName, DelayTime);
            Messenger.Default.Send(message, MessageTokens.AlgorithmStatisticsModel);
        }

        protected override void Summarize()
        {
            var status = !path.IsNull() ? AlgorithmStatus.Finished : AlgorithmStatus.Failed;
            string time = timer.Elapsed.ToString(@"mm\:ss\.ff");
            var message = new UpdateAlgorithmStatisticsMessage(AlgorithmStatisticsIndex,
                time, visitedVerticesCount, status, path.PathLength, path.PathCost);
            Messenger.Default.Send(message, MessageTokens.AlgorithmStatisticsModel);
        }

        protected override async void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            timer.Wait(DelayTime);
            string time = timer.Elapsed.ToString(@"mm\:ss\.ff");
            var message = new UpdateAlgorithmStatisticsMessage(AlgorithmStatisticsIndex, time, visitedVerticesCount);
            await Task.Run(() =>
            {
                Messenger.Default.Send(message, MessageTokens.AlgorithmStatisticsModel);
                base.OnVertexVisited(sender, e);
            });
        }

        protected override async void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            await Task.Run(() => base.OnVertexEnqueued(sender, e));
        }

        protected override void OnAlgorithmInterrupted(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmInterrupted(sender, e);
            Messenger.Default.Unregister<AlgorithmStatisticsIndexMessage>(this, MessageTokens.PathfindingModel, SetAlgorithmIndex);
            Messenger.Default.Unregister<DelayTimeChangedMessage>(this, MessageTokens.PathfindingModel, SetAlgorithmDelayTime);
        }

        protected override void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmFinished(sender, e);
            var message = new AlgorithmFinishedMessage(AlgorithmStatisticsIndex);
            Messenger.Default.Send(message, MessageTokens.AlgorithmStatisticsModel);
            Messenger.Default.Unregister<AlgorithmStatisticsIndexMessage>(this, MessageTokens.PathfindingModel, SetAlgorithmIndex);
            Messenger.Default.Unregister<DelayTimeChangedMessage>(this, MessageTokens.PathfindingModel, SetAlgorithmDelayTime);
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

        private int AlgorithmStatisticsIndex { get; set; }
    }
}
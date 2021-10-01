using Algorithm.Infrastructure.EventArguments;
using Common.Extensions;
using Common.Interface;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Logging.Interface;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
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

        public PathFindingViewModel(ILog log, IMainModel model, BaseEndPoints endPoints)
            : base(log, model, endPoints)
        {
            ConfirmPathFindAlgorithmChoice = new RelayCommand(
                ExecuteConfirmPathFindAlgorithmChoice,
                CanExecuteConfirmPathFindAlgorithmChoice);
            CancelPathFindAlgorithmChoice = new RelayCommand(ExecuteCloseWindowCommand);
            Messenger.Default.Register<InterruptAlgorithmMessage>(this, Constants.MessageToken, InterruptAlgorithm);
            Messenger.Default.Register<AlgorithmStatisticsIndexMessage>(this, Constants.MessageToken, SetAlgorithmIndex);
        }

        private void InterruptAlgorithm(InterruptAlgorithmMessage message)
        {
            algorithm.Interrupt();
        }

        private void SetAlgorithmIndex(AlgorithmStatisticsIndexMessage message)
        {
            Messenger.Default.Unregister<AlgorithmStatisticsIndexMessage>(this, Constants.MessageToken, SetAlgorithmIndex);
            AlgorithmStatisticsIndex = message.Index;
        }

        protected override void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmStarted(sender, e);
            Messenger.Default.Send(new AlgorithmStartedMessage(), Constants.MessageToken);
        }

        protected override void Summarize()
        {
            string stats = path.PathLength > 0 ? GetStatistics() : CouldntFindPathMsg;
            Messenger.Default.Send(new UpdateAlgorithmStatisticsMessage(AlgorithmStatisticsIndex, stats), Constants.MessageToken);
        }

        protected override async void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            timer.Wait(DelayTime);
            Messenger.Default.Send(new UpdateAlgorithmStatisticsMessage(AlgorithmStatisticsIndex, GetStatistics()), Constants.MessageToken);
            await Task.Run(() => base.OnVertexVisited(sender, e));
        }

        protected override async void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            await Task.Run(() => base.OnVertexEnqueued(sender, e));
        }

        protected override void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmFinished(sender, e);
            Messenger.Default.Send(new AlgorithmFinishedMessage(), Constants.MessageToken);
            Messenger.Default.Unregister<InterruptAlgorithmMessage>(this, Constants.MessageToken, InterruptAlgorithm);
            Messenger.Default.Unregister<AlgorithmStatisticsIndexMessage>(this, Constants.MessageToken, SetAlgorithmIndex);
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

using Common.Extensions.EnumerableExtensions;
using GalaSoft.MvvmLight.Messaging;
using GraphViewModel.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WPFVersion3D.Enums;
using WPFVersion3D.Extensions;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Messages;

namespace WPFVersion3D.ViewModel
{
    internal class AlgorithmStatisticsViewModel : INotifyPropertyChanged, IModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand InterruptSelelctedAlgorithmCommand { get; }
        public ICommand RemoveSelelctedAlgorithmCommand { get; }

        public AlgorithmViewModel SelectedAlgorithm { get; set; }

        private ObservableCollection<AlgorithmViewModel> statistics;
        public ObservableCollection<AlgorithmViewModel> Statistics
        {
            get => statistics ?? (statistics = new ObservableCollection<AlgorithmViewModel>());
            set { statistics = value; OnPropertyChanged(); }
        }

        public AlgorithmStatisticsViewModel()
        {
            InterruptSelelctedAlgorithmCommand = new RelayCommand(ExecuteInterruptSelectedAlgorithmCommand, CanExecuteInterruptSelectedAlgorithmCommand);
            RemoveSelelctedAlgorithmCommand = new RelayCommand(ExecuteRemoveFromStatisticsCommand, CanExecuteRemoveFromStatisticsCommand);
            Messenger.Default.Register<AlgorithmStartedMessage>(this, MessageTokens.AlgorithmStatisticsModel, OnAlgorithmStarted);
            Messenger.Default.Register<UpdateAlgorithmStatisticsMessage>(this, MessageTokens.AlgorithmStatisticsModel, UpdateAlgorithmStatistics);
            Messenger.Default.Register<InterruptAllAlgorithmsMessage>(this, MessageTokens.AlgorithmStatisticsModel, OnAllAlgorithmInterrupted);
            Messenger.Default.Register<ClearStatisticsMessage>(this, MessageTokens.AlgorithmStatisticsModel, OnClearStatistics);
            Messenger.Default.Register<AlgorithmStatusMessage>(this, MessageTokens.AlgorithmStatisticsModel, SetAlgorithmStatus);
        }

        private void SetAlgorithmStatus(AlgorithmStatusMessage message)
        {
            if (Statistics[message.Index].Status != AlgorithmStatuses.Interrupted)
            {
                Statistics[message.Index].Status = message.Status;
                SendIsAllFinishedMessage();
            }
        }

        private void OnAlgorithmStarted(AlgorithmStartedMessage message)
        {
            int index = Statistics.Count;
            var viewModel = new AlgorithmViewModel(message.Algorithm, message.AlgorithmName);
            Application.Current.Dispatcher.Invoke(() => Statistics.Add(viewModel));
            var msg = new AlgorithmIndexMessage(index);
            Messenger.Default.Send(msg, MessageTokens.PathfindingModel);
            SendIsAllFinishedMessage();
        }

        private void UpdateAlgorithmStatistics(UpdateAlgorithmStatisticsMessage message)
        {
            Application.Current.Dispatcher.Invoke(() => Statistics[message.Index].RecieveMessage(message));
        }

        private void OnAllAlgorithmInterrupted(InterruptAllAlgorithmsMessage message)
        {
            Statistics.ForEach(stat => stat.TryInterrupt());
        }

        private void OnClearStatistics(ClearStatisticsMessage message)
        {
            Statistics.Clear();
            SendIsAllFinishedMessage();
        }

        private void ExecuteRemoveFromStatisticsCommand(object param)
        {
            Statistics.Remove(SelectedAlgorithm);
            SendIsAllFinishedMessage();
        }

        private bool CanExecuteRemoveFromStatisticsCommand(object param)
        {
            return SelectedAlgorithm?.IsStarted() == false;
        }

        private void ExecuteInterruptSelectedAlgorithmCommand(object param)
        {
            SelectedAlgorithm?.TryInterrupt();
        }

        private bool CanExecuteInterruptSelectedAlgorithmCommand(object param)
        {
            return SelectedAlgorithm?.IsStarted() == true;
        }

        private void SendIsAllFinishedMessage()
        {
            var isAllFinished = Statistics.All(stat => !stat.IsStarted());
            var message = new IsAllAlgorithmsFinishedMessage(isAllFinished);
            Messenger.Default.Send(message, MessageTokens.MainModel);
        }
    }
}

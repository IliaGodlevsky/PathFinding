using Common.Extensions;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WPFVersion.Enums;
using WPFVersion.Extensions;
using WPFVersion.Infrastructure;
using WPFVersion.Messages;

namespace WPFVersion.ViewModel
{
    internal class AlgorithmStatisticsViewModel : INotifyPropertyChanged
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
            InterruptSelelctedAlgorithmCommand = new RelayCommand(ExecuteInterruptCurrentAlgorithmCommand, CanExecuteInterruptCurrentAlgorithmCommand);
            RemoveSelelctedAlgorithmCommand = new RelayCommand(ExecuteRemoveFromStatisticsCommand, CanExecuteRemoveFromStatisticsCommand);
            Messenger.Default.Register<AlgorithmStartedMessage>(this, Constants.MessageToken, OnAlgorithmStarted);
            Messenger.Default.Register<UpdateAlgorithmStatisticsMessage>(this, Constants.MessageToken, UpdateAlgorithmStatistics);
            Messenger.Default.Register<AlgorithmFinishedMessage>(this, Constants.MessageToken, OnAlgorithmFinished);
            Messenger.Default.Register<InterruptAllAlgorithmMessage>(this, Constants.MessageToken, OnAllAlgorithmInterrupted);
            Messenger.Default.Register<ClearStatisticsMessage>(this, Constants.MessageToken, OnClearStatistics);
        }

        private void OnAlgorithmStarted(AlgorithmStartedMessage message)
        {
            var viewModel = new AlgorithmViewModel(message.Algorithm, message.AlgorithmName);
            Application.Current.Dispatcher.Invoke(() => Statistics.Add(viewModel));
            var msg = new AlgorithmStatisticsIndexMessage(Statistics.Count - 1);
            Messenger.Default.Send(msg, Constants.MessageToken);
            SendStatusesMessage();
        }

        private void UpdateAlgorithmStatistics(UpdateAlgorithmStatisticsMessage message)
        {
            Statistics[message.Index].RecieveMessage(message);
            SendStatusesMessage();
        }

        private void OnAlgorithmFinished(AlgorithmFinishedMessage message)
        {
            Statistics[message.Index].Status = AlgorithmStatus.Finished;
            SendStatusesMessage();
        }

        private void OnAllAlgorithmInterrupted(InterruptAllAlgorithmMessage message)
        {
            Statistics.ForEach(stat => stat.TryInterrupt());
            SendStatusesMessage();
        }

        private void OnClearStatistics(ClearStatisticsMessage message)
        {
            Statistics.Clear();
            SendStatusesMessage();
        }

        private void ExecuteRemoveFromStatisticsCommand(object param)
        {
            Statistics.Remove(SelectedAlgorithm);
            SendStatusesMessage();
        }

        private bool CanExecuteRemoveFromStatisticsCommand(object param)
        {
            return SelectedAlgorithm?.IsStarted() == false;
        }

        private void ExecuteInterruptCurrentAlgorithmCommand(object param)
        {
            SelectedAlgorithm?.TryInterrupt();
            SendStatusesMessage();
        }

        private bool CanExecuteInterruptCurrentAlgorithmCommand(object param)
        {
            return SelectedAlgorithm?.IsStarted() == true;
        }

        private void SendStatusesMessage()
        {
            var statuses = Statistics.Select(stat => stat.Status).ToArray();
            var message = new AlgorithmStatusesMessage(statuses);
            Messenger.Default.Send(message, Constants.MessageToken);
        }
    }
}

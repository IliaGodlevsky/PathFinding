using Autofac;
using Common.Extensions.EnumerableExtensions;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Enums;
using WPFVersion3D.Extensions;
using WPFVersion3D.Infrastructure;
using WPFVersion3D.Messages;

namespace WPFVersion3D.ViewModel
{
    internal class AlgorithmStatisticsViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static Dispatcher Dispatcher => Application.Current.Dispatcher;

        private readonly IMessenger messenger;
        public ObservableCollection<AlgorithmViewModel> Statistics { get; set; }

        public ICommand InterruptSelelctedAlgorithmCommand { get; }

        public ICommand RemoveSelelctedAlgorithmCommand { get; }

        public AlgorithmViewModel SelectedAlgorithm { get; set; }

        public AlgorithmStatisticsViewModel()
        {
            Statistics = new ObservableCollection<AlgorithmViewModel>();
            messenger = DI.Container.Resolve<IMessenger>();
            InterruptSelelctedAlgorithmCommand = new RelayCommand(ExecuteInterruptSelectedAlgorithmCommand, CanExecuteInterruptSelectedAlgorithmCommand);
            RemoveSelelctedAlgorithmCommand = new RelayCommand(ExecuteRemoveFromStatisticsCommand, CanExecuteRemoveFromStatisticsCommand);
            messenger.Register<AlgorithmStartedMessage>(this, MessageTokens.AlgorithmStatisticsModel, OnAlgorithmStarted);
            messenger.Register<UpdateAlgorithmStatisticsMessage>(this, MessageTokens.AlgorithmStatisticsModel, UpdateAlgorithmStatistics);
            messenger.Register<InterruptAllAlgorithmsMessage>(this, MessageTokens.AlgorithmStatisticsModel, OnAllAlgorithmInterrupted);
            messenger.Register<ClearStatisticsMessage>(this, MessageTokens.AlgorithmStatisticsModel, OnClearStatistics);
            messenger.Register<AlgorithmStatusMessage>(this, MessageTokens.AlgorithmStatisticsModel, SetAlgorithmStatus);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            var viewModel = new AlgorithmViewModel(message.Value);
            Dispatcher.Invoke(() => Statistics.Add(viewModel));
            var msg = new AlgorithmIndexMessage(index);
            messenger.Forward(msg, MessageTokens.PathfindingModel);
            SendIsAllFinishedMessage();
        }

        private void UpdateAlgorithmStatistics(UpdateAlgorithmStatisticsMessage message)
        {
            Dispatcher.Invoke(() => Statistics[message.Index].RecieveMessage(message));
        }

        private void OnAllAlgorithmInterrupted(InterruptAllAlgorithmsMessage message)
        {
            Statistics.ForEach(stat => stat.InterruptIfStarted());
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
            return SelectedAlgorithm?.IsStarted == false;
        }

        private void ExecuteInterruptSelectedAlgorithmCommand(object param)
        {
            SelectedAlgorithm?.InterruptIfStarted();
        }

        private bool CanExecuteInterruptSelectedAlgorithmCommand(object param)
        {
            return SelectedAlgorithm?.IsStarted == true;
        }

        private void SendIsAllFinishedMessage()
        {
            var isAllFinished = Statistics.All(stat => !stat.IsStarted);
            var message = new IsAllAlgorithmsFinishedMessage(isAllFinished);
            messenger.Forward(message, MessageTokens.MainModel);
        }

        public void Dispose()
        {
            messenger.Unregister(this);
            Statistics.Clear();
            SelectedAlgorithm = null;
        }
    }
}
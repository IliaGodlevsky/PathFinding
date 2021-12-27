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
using WPFVersion.DependencyInjection;
using WPFVersion.Enums;
using WPFVersion.Extensions;
using WPFVersion.Infrastructure;
using WPFVersion.Messages;
using WPFVersion.Model;

namespace WPFVersion.ViewModel
{
    internal class AlgorithmStatisticsViewModel : INotifyPropertyChanged, IDisposable
    {
        private Dispatcher Dispatcher => Application.Current.Dispatcher;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand InterruptSelelctedAlgorithmCommand { get; }
        public ICommand RemoveSelelctedAlgorithmCommand { get; }
        public ICommand VisualizeCommand { get; }

        public AlgorithmViewModel SelectedAlgorithm { get; set; }
        public ObservableCollection<AlgorithmViewModel> Algorithms { get; set; }

        public AlgorithmStatisticsViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            Algorithms = new ObservableCollection<AlgorithmViewModel>();
            VisualizeCommand = new RelayCommand(ExecuteVisualizeCommand, CanExecuteVisualizeCommand);
            InterruptSelelctedAlgorithmCommand = new RelayCommand(ExecuteInterruptSelectedAlgorithmCommand, CanExecuteInterruptSelectedAlgorithmCommand);
            RemoveSelelctedAlgorithmCommand = new RelayCommand(ExecuteRemoveFromStatisticsCommand, CanExecuteRemoveFromStatisticsCommand);
            messenger.Register<AlgorithmStartedMessage>(this, MessageTokens.AlgorithmStatisticsModel, OnAlgorithmStarted);
            messenger.Register<UpdateStatisticsMessage>(this, MessageTokens.AlgorithmStatisticsModel, UpdateAlgorithmStatistics);
            messenger.Register<InterruptAllAlgorithmsMessage>(this, MessageTokens.AlgorithmStatisticsModel, OnAllAlgorithmInterrupted);
            messenger.Register<ClearStatisticsMessage>(this, MessageTokens.AlgorithmStatisticsModel, OnClearStatistics);
            messenger.Register<AlgorithmStatusMessage>(this, MessageTokens.AlgorithmStatisticsModel, SetAlgorithmStatistics);
            messenger.Register<GraphCreatedMessage>(this, MessageTokens.AlgorithmStatisticsModel, NewGraphCreated);
        }

        private void SetAlgorithmStatistics(AlgorithmStatusMessage message)
        {
            if (Algorithms[message.Index].Status != AlgorithmStatus.Interrupted)
            {
                Algorithms[message.Index].Status = message.Status;
                SendIsAllAlgorithmsFinishedMessage();
            }
        }

        private void OnAlgorithmStarted(AlgorithmStartedMessage message)
        {
            int index = Algorithms.Count;
            var msg = new AlgorithmIndexMessage(index);
            messenger.Forward(msg, MessageTokens.PathfindingModel);
            var viewModel = new AlgorithmViewModel(message, index);
            Dispatcher.Invoke(() => Algorithms.Add(viewModel));
            SendIsAllAlgorithmsFinishedMessage();
        }

        private void UpdateAlgorithmStatistics(UpdateStatisticsMessage message)
        {
            Dispatcher.Invoke(() => Algorithms[message.Index].RecieveMessage(message));
        }

        private void OnAllAlgorithmInterrupted(InterruptAllAlgorithmsMessage message)
        {
            Algorithms.ForEach(stat => stat.TryInterrupt());
        }

        private void NewGraphCreated(GraphCreatedMessage message)
        {
            if (visualizationModel != null)
            {
                visualizationModel.Dispose();
            }
            visualizationModel = new PathfindingVisualizationModel(message.Graph);
        }

        private void OnClearStatistics(ClearStatisticsMessage message)
        {
            Algorithms.Clear();
            visualizationModel?.Clear();
            SendIsAllAlgorithmsFinishedMessage();
        }

        private void ExecuteRemoveFromStatisticsCommand(object param)
        {
            visualizationModel?.Remove(SelectedAlgorithm.Algorithm);
            Algorithms.Remove(SelectedAlgorithm);
            SendIsAllAlgorithmsFinishedMessage();
        }

        private void ExecuteVisualizeCommand(object param)
        {
            visualizationModel.Visualize(SelectedAlgorithm.Algorithm);
        }

        private void ExecuteInterruptSelectedAlgorithmCommand(object param)
        {
            SelectedAlgorithm?.TryInterrupt();
        }

        private bool CanExecuteRemoveFromStatisticsCommand(object param)
        {
            return IsAllFinished;
        }

        private bool CanExecuteVisualizeCommand(object param)
        {
            return IsAllFinished
                && SelectedAlgorithm != null
                && visualizationModel != null;
        }

        private bool CanExecuteInterruptSelectedAlgorithmCommand(object param)
        {
            return SelectedAlgorithm?.IsStarted() == true;
        }

        private void SendIsAllAlgorithmsFinishedMessage()
        {
            var message = new IsAllAlgorithmsFinishedMessage(IsAllFinished);
            messenger.Forward(message, MessageTokens.MainModel);
        }

        public void Dispose()
        {
            messenger.Unregister(this);
            Algorithms.Clear();
            SelectedAlgorithm = null;
            if (visualizationModel != null)
            {
                visualizationModel.Dispose();
                visualizationModel = null;
            }
        }

        private bool IsAllFinished => Algorithms.All(stat => !stat.IsStarted());

        private PathfindingVisualizationModel visualizationModel;
        private readonly IMessenger messenger;
    }
}
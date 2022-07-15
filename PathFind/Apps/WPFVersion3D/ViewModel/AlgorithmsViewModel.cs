using Autofac;
using Commands.Extensions;
using Commands.Interfaces;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Extensions;
using WPFVersion3D.Infrastructure.Commands;
using WPFVersion3D.Interface;
using WPFVersion3D.Messages.ActionMessages;
using WPFVersion3D.Messages.PassValueMessages;
using WPFVersion3D.Model;

namespace WPFVersion3D.ViewModel
{
    internal class AlgorithmsViewModel : IDisposable
    {
        private static Dispatcher Dispatcher => Application.Current.Dispatcher;

        private readonly IMessenger messenger;

        private PathfindingVisualizationModel visualizationModel;

        private bool IsAllAlgorithmsFinished => AlgorithmModels.All(stat => !stat.IsStarted);

        public ICommand VisualizeCommand { get; }

        public AlgorithmViewModel SelectedAlgorithm { get; set; }

        public ObservableCollection<AlgorithmViewModel> AlgorithmModels { get; }

        public AlgorithmsViewModel()
        {
            AlgorithmModels = new ObservableCollection<AlgorithmViewModel>();
            messenger = DI.Container.Resolve<IMessenger>();
            VisualizeCommand = new RelayCommand(ExecuteVisualizeCommand, CanExecuteVisualizeCommand);
            messenger.Register<AlgorithmStartedMessage>(this, OnAlgorithmStarted);
            messenger.Register<UpdateAlgorithmStatisticsMessage>(this, UpdateAlgorithmStatistics);
            messenger.Register<ClearStatisticsMessage>(this, OnClearStatistics);
            messenger.Register<AlgorithmStatusMessage>(this, SetAlgorithmStatus);
            messenger.Register<RemoveAlgorithmMessage>(this, OnAlgorithmRemoved);
            messenger.Register<IExecutable<AlgorithmViewModel>>(this, true, OnAllAlgorithmExecution);
            messenger.Register<GraphCreatedMessage>(this, NewGraphCreated);
        }

        private void SetAlgorithmStatus(AlgorithmStatusMessage message)
        {
            if (AlgorithmModels[message.Index].Status != AlgorithmViewModel.Interrupted)
            {
                AlgorithmModels[message.Index].Status = message.Status;
                messenger.Send(new IsAllAlgorithmsFinishedMessage(IsAllAlgorithmsFinished));
            }
        }

        private void OnAlgorithmStarted(AlgorithmStartedMessage message)
        {
            int index = AlgorithmModels.Count;
            var viewModel = new AlgorithmViewModel(message.Value);
            Dispatcher.Invoke(() => AlgorithmModels.Add(viewModel));
            messenger.Send(new AlgorithmIndexMessage(index));
            messenger.Send(new IsAllAlgorithmsFinishedMessage(IsAllAlgorithmsFinished));
        }

        private void UpdateAlgorithmStatistics(UpdateAlgorithmStatisticsMessage message)
        {
            Dispatcher.Invoke(() => AlgorithmModels[message.Index].UpdateStatistics(message));
        }

        private void OnAllAlgorithmExecution(IExecutable<AlgorithmViewModel> message)
        {
            message.Execute(AlgorithmModels);
        }

        private void OnClearStatistics(ClearStatisticsMessage message)
        {
            AlgorithmModels.Clear();
            messenger.Send(new IsAllAlgorithmsFinishedMessage(IsAllAlgorithmsFinished));
        }

        private void NewGraphCreated(GraphCreatedMessage message)
        {
            if (visualizationModel != null)
            {
                visualizationModel.Dispose();
            }
            visualizationModel = new PathfindingVisualizationModel(message.Value);
        }

        private void OnAlgorithmRemoved(RemoveAlgorithmMessage message)
        {
            AlgorithmModels.Remove(message.Value);
            messenger.Send(new IsAllAlgorithmsFinishedMessage(IsAllAlgorithmsFinished));
        }

        private void ExecuteVisualizeCommand(object param)
        {
            visualizationModel.Execute(SelectedAlgorithm.Algorithm);
        }

        private bool CanExecuteVisualizeCommand(object param)
        {
            return IsAllAlgorithmsFinished && SelectedAlgorithm != null && visualizationModel != null;
        }

        public void Dispose()
        {
            messenger.Unregister(this);
            AlgorithmModels.Clear();
        }
    }
}
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.WPF._2D.Infrastructure;
using Pathfinding.App.WPF._2D.Messages.ActionMessages;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using Pathfinding.App.WPF._2D.Model;
using Shared.Executable;
using Shared.Executable.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D.ViewModel
{
    internal class AlgorithmsViewModel : IDisposable
    {
        private PathfindingVisualizationModel visualizationModel;
        private readonly IMessenger messenger;

        public ICommand VisualizeCommand { get; }

        private bool IsAllFinished => Algorithms.All(algorithm => !algorithm.IsStarted);

        private Dispatcher Dispatcher => Application.Current.Dispatcher;

        public AlgorithmViewModel SelectedAlgorithm { get; set; }

        public ObservableCollection<AlgorithmViewModel> Algorithms { get; }

        public AlgorithmsViewModel()
        {
            messenger = DI.Container.Resolve<IMessenger>();
            Algorithms = new ObservableCollection<AlgorithmViewModel>();
            VisualizeCommand = new RelayCommand(ExecuteVisualizeCommand, CanExecuteVisualizeCommand);
            messenger.Register<AlgorithmStartedMessage>(this, OnAlgorithmStarted);
            messenger.Register<UpdateStatisticsMessage>(this, UpdateAlgorithmStatistics);
            messenger.Register<IExecutable<AlgorithmViewModel>>(this, true, OnAllAlgorithmAction);
            messenger.Register<ClearStatisticsMessage>(this, OnClearStatistics);
            messenger.Register<AlgorithmStatusMessage>(this, SetAlgorithmStatistics);
            messenger.Register<GraphCreatedMessage>(this, NewGraphCreated);
            messenger.Register<RemoveAlgorithmMessage>(this, OnAlgorithmRemoved);
        }

        private void SetAlgorithmStatistics(AlgorithmStatusMessage message)
        {
            if (!Algorithms[message.Index].IsInterrupted)
            {
                Algorithms[message.Index].Status = message.Status;
                messenger.Send(new IsAllAlgorithmsFinishedMessage(IsAllFinished));
            }
        }

        private void OnAlgorithmStarted(AlgorithmStartedMessage message)
        {
            int index = Algorithms.Count;
            messenger.Send(new AlgorithmIndexMessage(index));
            var viewModel = new AlgorithmViewModel(message, index);
            Dispatcher.Invoke(() => Algorithms.Add(viewModel));
            messenger.Send(new IsAllAlgorithmsFinishedMessage(IsAllFinished));
        }

        private void UpdateAlgorithmStatistics(UpdateStatisticsMessage message)
        {
            var model = Algorithms[message.Index];
            Dispatcher.Invoke(() =>
            {
                model.Time = message.Time;
                model.PathCost = message.PathCost;
                model.PathLength = message.PathLength;
                model.VisitedVerticesCount = message.VisitedVertices;
            });
        }

        private void OnAllAlgorithmAction(IExecutable<AlgorithmViewModel> message)
        {
            message.Execute(Algorithms);
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
            messenger.Send(new IsAllAlgorithmsFinishedMessage(IsAllFinished));
        }

        private void ExecuteRemoveFromStatisticsCommand(object param)
        {
            visualizationModel?.Remove(SelectedAlgorithm.Algorithm);
            Algorithms.Remove(SelectedAlgorithm);
            messenger.Send(new IsAllAlgorithmsFinishedMessage(IsAllFinished));
        }

        private void ExecuteVisualizeCommand(object param)
        {
            visualizationModel.Execute(SelectedAlgorithm.Algorithm);
        }

        private bool CanExecuteVisualizeCommand(object param)
        {
            return IsAllFinished && SelectedAlgorithm != null && visualizationModel != null;
        }

        private void OnAlgorithmRemoved(RemoveAlgorithmMessage model)
        {
            Algorithms.Remove(model.Model);
            messenger.Send(new IsAllAlgorithmsFinishedMessage(IsAllFinished));
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
    }
}
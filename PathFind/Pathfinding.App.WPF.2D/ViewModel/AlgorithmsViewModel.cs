using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.History;
using Pathfinding.App.WPF._2D.Infrastructure;
using Pathfinding.App.WPF._2D.Messages.ActionMessages;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using Pathfinding.App.WPF._2D.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.Visualization.Extensions;
using Shared.Executable;
using Shared.Executable.Extensions;
using Shared.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D.ViewModel
{
    internal class AlgorithmsViewModel : IDisposable
    {
        private readonly History<PathfindingHistoryVolume> history;
        private readonly IMessenger messenger;

        public ICommand VisualizeCommand { get; }

        private bool IsAllFinished => Algorithms.All(algorithm => !algorithm.IsStarted);

        private Dispatcher Dispatcher => Application.Current.Dispatcher;

        private IGraph<Vertex> Graph { get; set; }

        public AlgorithmViewModel SelectedAlgorithm { get; set; }

        public ObservableCollection<AlgorithmViewModel> Algorithms { get; }

        public AlgorithmsViewModel()
        {
            history = new History<PathfindingHistoryVolume>();
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
            messenger.Register<PathFoundMessage>(this, PathFound);
            messenger.Register<PathfindingRangeChosenMessage>(this, AddPathfindingRange);
        }

        private void SetAlgorithmStatistics(AlgorithmStatusMessage message)
        {
            var algorithm = Algorithms.FirstOrDefault(a => a.Id == message.Id);
            if (!algorithm.IsInterrupted)
            {
                algorithm.Status = message.Status;
                messenger.Send(new IsAllAlgorithmsFinishedMessage(IsAllFinished));
            }
        }

        private void AddPathfindingRange(PathfindingRangeChosenMessage message)
        {
            history.AddPathfindingRange(message.Id, message.PathfindingRange);
        }

        private void PathFound(PathFoundMessage message)
        {
            history.AddPath(message.Id, message.Path);
        }

        private void OnAlgorithmStarted(AlgorithmStartedMessage message)
        {
            message.Algorithm.VertexVisited += OnVertexVisited;
            var viewModel = new AlgorithmViewModel(message);
            Dispatcher.Invoke(() => Algorithms.Add(viewModel));
            history.AddObstacles(message.Algorithm.Id, Graph.GetObstaclesCoordinates());
            messenger.Send(new IsAllAlgorithmsFinishedMessage(IsAllFinished));
        }

        private void UpdateAlgorithmStatistics(UpdateStatisticsMessage message)
        {
            var algorithm = Algorithms.FirstOrDefault(a => a.Id == message.Id);
            Dispatcher.Invoke(() =>
            {
                algorithm.Time = message.Time;
                algorithm.PathCost = message.PathCost;
                algorithm.PathLength = message.PathLength;
                algorithm.VisitedVerticesCount = message.VisitedVertices;
            });
        }

        private void OnAllAlgorithmAction(IExecutable<AlgorithmViewModel> message)
        {
            message.Execute(Algorithms);
        }

        private void NewGraphCreated(GraphCreatedMessage message)
        {
            history.Clear();
            Algorithms.Clear();
            Graph = message.Graph;
        }

        private void OnClearStatistics(ClearStatisticsMessage message)
        {
            Algorithms.Clear();
            history.Clear();
            messenger.Send(new IsAllAlgorithmsFinishedMessage(IsAllFinished));
        }

        private void ExecuteRemoveFromStatisticsCommand(object param)
        {
            history.Remove(SelectedAlgorithm.Id);
            Algorithms.Remove(SelectedAlgorithm);
            messenger.Send(new IsAllAlgorithmsFinishedMessage(IsAllFinished));
        }

        private void ExecuteVisualizeCommand(object param)
        {
            Graph.ForEach(vertex => vertex.VisualizeAsRegular());
            history.VisualizeHistory(SelectedAlgorithm.Id, Graph);
        }

        private bool CanExecuteVisualizeCommand(object param)
        {
            return IsAllFinished && SelectedAlgorithm != null;
        }

        private void OnAlgorithmRemoved(RemoveAlgorithmMessage message)
        {
            var model = Algorithms.FirstOrDefault(a => a.Id == message.Id);
            Algorithms.Remove(model);
            messenger.Send(new IsAllAlgorithmsFinishedMessage(IsAllFinished));
        }

        private async void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            await Task.Run(() =>
            {
                if (sender is PathfindingProcess key)
                {
                    history.AddVisited(key.Id, e.Current);
                }
            });
        }

        public void Dispose()
        {
            messenger.Unregister(this);
            Algorithms.Clear();
            SelectedAlgorithm = null;
            history.Clear();
        }
    }
}
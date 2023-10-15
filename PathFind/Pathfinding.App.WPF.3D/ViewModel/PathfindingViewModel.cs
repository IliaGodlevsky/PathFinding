using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.WPF._3D.DependencyInjection;
using Pathfinding.App.WPF._3D.Extensions;
using Pathfinding.App.WPF._3D.Infrastructure.Commands;
using Pathfinding.App.WPF._3D.Interface;
using Pathfinding.App.WPF._3D.Messages.PassValueMessages;
using Pathfinding.App.WPF._3D.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Models;
using Shared.Extensions;
using Shared.Process.EventArguments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pathfinding.App.WPF._3D.ViewModel
{
    public class PathFindingViewModel : PathFindingModel<Vertex3D>, IViewModel, IDisposable
    {
        public event Action WindowClosed;

        private readonly IMessenger messenger;

        public ICommand FindPathCommand { get; }

        public ICommand CancelFindPathCommand { get; }

        private Guid Id { get; set; }

        public PathFindingViewModel(IPathfindingRangeBuilder<Vertex3D> rangeBuilder,
            IEnumerable<IAlgorithmFactory<PathfindingProcess>> algorithmFactories, ILog log, ICache<IGraph<Vertex3D>> graphCache)
            : base(rangeBuilder.Range, algorithmFactories, graphCache.Cache, log)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            FindPathCommand = new RelayCommand(ExecutePathFindCommand, CanExecutePathFindCommand);
            CancelFindPathCommand = new RelayCommand(ExecuteCloseWindowCommand);
            Delay = Constants.AlgorithmDelayValueRange.LowerValueOfRange;
        }

        protected override void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            messenger.Send(new AlgorithmStartedMessage(algorithm));
            messenger.Send(new PathfindingRangeChosenMessage(range, Id));
        }

        protected override void SummarizePathfindingResults()
        {
            var status = Path.Count > 0 ? AlgorithmViewModel.Finished : AlgorithmViewModel.Failed;
            string time = timer.Elapsed.ToString(@"mm\:ss\.fff");
            messenger.Send(new UpdateAlgorithmStatisticsMessage(Id, time, visitedVerticesCount, Path.Count, Path.Cost));
            messenger.Send(new AlgorithmStatusMessage(status, Id));
            messenger.Send(new PathFoundMessage(Path, Id));
        }

        protected override async void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            Delay.Wait();
            string time = timer.Elapsed.ToString(@"mm\:ss\.fff");
            var message = new UpdateAlgorithmStatisticsMessage(Id, time, visitedVerticesCount);
            await Task.Run(Graph.Get(e.Current).VisualizeAsVisited).ConfigureAwait(false);
            await messenger.SendAsync(message).ConfigureAwait(false);
            visitedVerticesCount++;
        }

        protected override async void OnVertexEnqueued(object sender, PathfindingEventArgs e)
        {
            await Task.Run(Graph.Get(e.Current).VisualizeAsEnqueued).ConfigureAwait(false);
        }

        protected override void OnAlgorithmInterrupted(object sender, ProcessEventArgs e)
        {
            messenger.Send(AlgorithmStatusMessage.Interrupted(Id));
        }

        protected override void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            messenger.Unregister(this);
        }

        protected override void SubscribeOnAlgorithmEvents(PathfindingProcess algorithm)
        {
            messenger.Send(new SubscribeOnAlgorithmEventsMessage(algorithm, IsVisualizationRequired));
            algorithm.Paused += OnAlgorithmPaused;
            algorithm.Resumed += OnAlgorithmUnpaused;
            base.SubscribeOnAlgorithmEvents(algorithm);
        }

        private void OnAlgorithmPaused(object sender, ProcessEventArgs e)
        {
            messenger.Send(AlgorithmStatusMessage.Paused(Id));
        }

        private void OnAlgorithmUnpaused(object sender, ProcessEventArgs e)
        {
            messenger.Send(AlgorithmStatusMessage.Started(Id));
        }

        private void ExecuteCloseWindowCommand(object param)
        {
            WindowClosed?.Invoke();
        }

        private void ExecutePathFindCommand(object param)
        {
            ExecuteCloseWindowCommand(null);
            base.FindPath();
            Id = algorithm.Id;
        }

        private bool CanExecutePathFindCommand(object param)
        {
            return Algorithm != null;
        }

        public void Dispose()
        {
            WindowClosed = null;
        }
    }
}
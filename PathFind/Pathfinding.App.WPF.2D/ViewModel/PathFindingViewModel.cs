using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.App.WPF._2D.Extensions;
using Pathfinding.App.WPF._2D.Infrastructure;
using Pathfinding.App.WPF._2D.Interface;
using Pathfinding.App.WPF._2D.Messages.ActionMessages;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using Pathfinding.App.WPF._2D.Model;
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
using WPFVersion.DependencyInjection;

namespace Pathfinding.App.WPF._2D.ViewModel
{
    public class PathFindingViewModel : PathFindingModel<Vertex>, IViewModel, IDisposable
    {
        public event Action WindowClosed;

        private readonly IMessenger messenger;

        private Guid Id { get; set; }

        public ICommand ConfirmPathFindAlgorithmChoice { get; }

        public ICommand CancelPathFindAlgorithmChoice { get; }

        public PathFindingViewModel(IPathfindingRangeBuilder<Vertex> rangeBuilder,
            IEnumerable<IAlgorithmFactory<PathfindingProcess>> algorithmFactories, ICache<IGraph<Vertex>> graphCache, ILog log)
            : base(rangeBuilder.Range, algorithmFactories, graphCache.Cached, log)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            ConfirmPathFindAlgorithmChoice = new RelayCommand(ExecuteConfirmPathFindAlgorithmChoice, CanExecuteConfirmPathFindAlgorithmChoice);
            CancelPathFindAlgorithmChoice = new RelayCommand(ExecuteCloseWindowCommand);
            messenger.Register<DelayTimeChangedMessage>(this, SetAlgorithmDelayTime);
            Delay = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange;
        }

        private void SetAlgorithmDelayTime(DelayTimeChangedMessage message)
        {
            if (Id == message.Id)
            {
                Delay = message.DelayTime;
            }
        }

        protected override void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            messenger.Send(new AlgorithmStartedMessage(algorithm, Delay));
            messenger.Send(new PathfindingRangeChosenMessage(algorithm.Id, range));
        }

        protected override void SummarizePathfindingResults()
        {
            string time = timer.Elapsed.ToString(@"mm\:ss\.fff");
            var updateMessage = new UpdateStatisticsMessage(Id, time, visitedVerticesCount, Path.Count, Path.Cost);
            messenger.Send(updateMessage);
            messenger.Send(new AlgorithmStatusMessage(Path.ToStatus(), Id));
            messenger.Send(new PathFoundMessage(algorithm.Id, Path));
        }

        protected override async void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            Delay.Wait();
            visitedVerticesCount++;
            string time = timer.Elapsed.ToString(@"mm\:ss\.fff");
            var message = new UpdateStatisticsMessage(Id, time, visitedVerticesCount);
            await Task.Run(() => base.OnVertexVisited(sender, e))
                .ContinueWith(task => messenger.SendAsync(message))
                .ConfigureAwait(false);
        }

        protected override async void OnVertexEnqueued(object sender, PathfindingEventArgs e)
        {
            await Task.Run(() => base.OnVertexEnqueued(sender, e)).ConfigureAwait(false);
        }

        protected override void OnAlgorithmInterrupted(object sender, ProcessEventArgs e)
        {
            var message = new AlgorithmStatusMessage(AlgorithmViewModel.Interrupted, Id);
            messenger.Send(message);
        }

        protected void OnAlgorithmPaused(object sender, ProcessEventArgs e)
        {
            var message = new AlgorithmStatusMessage(AlgorithmViewModel.Paused, Id);
            messenger.Send(message);
        }

        protected void OnAlgorithmUnpaused(object sender, ProcessEventArgs e)
        {
            var message = new AlgorithmStatusMessage(AlgorithmViewModel.Started, Id);
            messenger.Send(message);
        }

        protected override void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            messenger.Unregister(this);
        }

        private void ExecuteCloseWindowCommand(object param)
        {
            WindowClosed?.Invoke();
        }

        private void ExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            ExecuteCloseWindowCommand(null);
            base.FindPath();
            Id = algorithm.Id;
        }

        protected override void SubscribeOnAlgorithmEvents(PathfindingProcess algorithm)
        {
            algorithm.Paused += OnAlgorithmPaused;
            algorithm.Resumed += OnAlgorithmUnpaused;
            base.SubscribeOnAlgorithmEvents(algorithm);
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            return Algorithm != null;
        }

        public void Dispose()
        {
            WindowClosed = null;
        }
    }
}
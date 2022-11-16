using GalaSoft.MvvmLight.Messaging;
using Pathfinding.Logging.Interface;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using WPFVersion.DependencyInjection;
using Pathfinding.App.WPF._2D.Model;
using Pathfinding.App.WPF._2D.Messages.DataMessages;
using Pathfinding.App.WPF._2D.Interface;
using Pathfinding.App.WPF._2D.Messages.ActionMessages;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Shared.Process.EventArguments;
using Pathfinding.Visualization.Models;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.App.WPF._2D.Infrastructure;
using Pathfinding.AlgorithmLib.Core.Events;
using Shared.Extensions;
using Autofac;
using Pathfinding.App.WPF._2D.Extensions;

namespace Pathfinding.App.WPF._2D.ViewModel
{
    public class PathFindingViewModel : PathFindingModel<Vertex>, IViewModel, IDisposable
    {
        public event Action WindowClosed;

        private readonly IMessenger messenger;

        private int Index { get; set; }

        public ICommand ConfirmPathFindAlgorithmChoice { get; }

        public ICommand CancelPathFindAlgorithmChoice { get; }

        public PathFindingViewModel(PathfindingRangeAdapter<Vertex> adapter,
            IEnumerable<IAlgorithmFactory<PathfindingProcess>> algorithmFactories, ICache<Graph2D<Vertex>> graphCache, ILog log)
            : base(adapter, algorithmFactories, graphCache.Cached, log)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            ConfirmPathFindAlgorithmChoice = new RelayCommand(ExecuteConfirmPathFindAlgorithmChoice, CanExecuteConfirmPathFindAlgorithmChoice);
            CancelPathFindAlgorithmChoice = new RelayCommand(ExecuteCloseWindowCommand);
            messenger.Register<AlgorithmIndexMessage>(this, SetAlgorithmIndex);
            messenger.Register<DelayTimeChangedMessage>(this, SetAlgorithmDelayTime);
            Delay = Constants.AlgorithmDelayTimeValueRange.LowerValueOfRange;
        }

        private void SetAlgorithmIndex(AlgorithmIndexMessage message)
        {
            messenger.Unregister<AlgorithmIndexMessage>(this, SetAlgorithmIndex);
            Index = message.Index;
        }

        private void SetAlgorithmDelayTime(DelayTimeChangedMessage message)
        {
            if (Index == message.Index)
            {
                Delay = message.DelayTime;
            }
        }

        protected override void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            messenger.Send(new AlgorithmStartedMessage(algorithm, Delay));
            messenger.Send(new PathfindingRangeChosenMessage(algorithm, range));
        }

        protected override void SummarizePathfindingResults()
        {
            string time = timer.Elapsed.ToString(@"mm\:ss\.fff");
            var updateMessage = new UpdateStatisticsMessage(Index, time, visitedVerticesCount, Path.Count, Path.Cost);
            messenger.Send(updateMessage);
            messenger.Send(new AlgorithmStatusMessage(Path.ToStatus(), Index));
            messenger.Send(new PathFoundMessage(algorithm, Path));
        }

        protected override async void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            Delay.Wait();
            string time = timer.Elapsed.ToString(@"mm\:ss\.fff");
            var message = new UpdateStatisticsMessage(Index, time, visitedVerticesCount);
            await messenger.SendAsync(message).ConfigureAwait(false);
            visitedVerticesCount++;
        }

        protected override void OnVertexEnqueued(object sender, PathfindingEventArgs e) { }

        protected override void OnAlgorithmInterrupted(object sender, ProcessEventArgs e)
        {
            var message = new AlgorithmStatusMessage(AlgorithmViewModel.Interrupted, Index);
            messenger.Send(message);
        }

        protected void OnAlgorithmPaused(object sender, ProcessEventArgs e)
        {
            var message = new AlgorithmStatusMessage(AlgorithmViewModel.Paused, Index);
            messenger.Send(message);
        }

        protected void OnAlgorithmUnpaused(object sender, ProcessEventArgs e)
        {
            var message = new AlgorithmStatusMessage(AlgorithmViewModel.Started, Index);
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
        }

        protected override void SubscribeOnAlgorithmEvents(PathfindingProcess algorithm)
        {
            var message = new SubscribeOnAlgorithmEventsMessage(algorithm, IsVisualizationRequired);
            messenger.Send(message);
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
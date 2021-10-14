using Algorithm.Factory;
using Algorithm.Infrastructure.EventArguments;
using Common.Extensions;
using Common.Interface;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WindowsFormsVersion.Messeges;

namespace WindowsFormsVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel, IModel, IViewModel
    {
        public event EventHandler WindowClosed;

        public PathFindingViewModel(ILog log, IGraph graph,
            BaseEndPoints endPoints, IEnumerable<IAlgorithmFactory> algorithmFactories)
            : base(log, graph, endPoints, algorithmFactories)
        {

        }

        protected override void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmFinished(sender, e);
            Messenger.Default.Send(new AlgorithmStatusMessage(false), MessageTokens.MainModel);
        }

        protected override void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmStarted(sender, e);
            Messenger.Default.Send(new AlgorithmStatusMessage(true), MessageTokens.MainModel);
        }

        protected override void SummarizePathfindingResults()
        {
            string statistics = path.PathLength > 0 ? Statistics : CouldntFindPath;
            var message = new UpdateStatisticsMessage(statistics);
            Messenger.Default.Send(message, MessageTokens.MainModel);
        }

        protected override void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            Stopwatch.StartNew().Pause(DelayTime).Cancel();
            base.OnVertexVisited(sender, e);
            Messenger.Default.Send(new UpdateStatisticsMessage(Statistics), MessageTokens.MainModel);
        }

        protected override void OnAlgorithmInterrupted(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmInterrupted(sender, e);
            Messenger.Default.Send(new AlgorithmStatusMessage(false), MessageTokens.MainModel);
        }

        public void StartPathfinding(object sender, EventArgs e)
        {
            if (CanExecuteConfirmPathFindAlgorithmChoice())
            {
                WindowClosed?.Invoke(this, EventArgs.Empty);
                WindowClosed = null;
                FindPath();
            }
        }

        public void CancelPathFinding(object sender, EventArgs e)
        {
            WindowClosed?.Invoke(this, EventArgs.Empty);
            WindowClosed = null;
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice()
        {
            return Algorithms.Any(item => item.Item2 == Algorithm);
        }

        private string Statistics
        {
            get
            {
                string timerInfo = timer.ToFormattedString();
                string description = Algorithms.FirstOrDefault(item => item.Item2 == Algorithm).Item1;
                string pathfindingInfo = string.Format(Format, PathfindingInfo);
                return string.Join("\t", description, timerInfo, pathfindingInfo);
            }
        }

        private object[] PathfindingInfo => new object[] { path.PathLength, path.PathCost, visitedVerticesCount };

        private readonly string Format = "Steps: {0}  Path cost: {1}  Visited: {2}";
        private readonly string CouldntFindPath = "Could't fing path";
    }
}

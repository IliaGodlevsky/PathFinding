﻿using Algorithm.Infrastructure.EventArguments;
using Common.Extensions;
using Common.Interface;
using GraphLib.Base;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Logging.Interface;
using System;
using System.Linq;

namespace WindowsFormsVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel, IModel, IViewModel
    {
        public event EventHandler WindowClosed;

        public PathFindingViewModel(ILog log, IMainModel model, BaseEndPoints endPoints)
            : base(log, model, endPoints)
        {

        }

        protected override void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmFinished(sender, e);
            if (mainViewModel is MainWindowViewModel mainModel)
            {
                mainModel.IsPathfindingStarted = false;
            }
        }

        protected override void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmStarted(sender, e);
            if (mainViewModel is MainWindowViewModel mainModel)
            {
                mainModel.IsPathfindingStarted = true;
            }
        }

        protected override void Summarize()
        {
            if (mainViewModel is MainWindowViewModel mainModel)
            {
                mainModel.PathFindingStatistics
                    = path.PathLength > 0 ? GetStatistics() : CouldntFindPathMsg;
            }
        }

        protected override void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            base.OnVertexVisited(sender, e);
            if (mainViewModel is MainWindowViewModel mainModel)
            {
                mainModel.PathFindingStatistics = GetStatistics();
            }
            timer.Wait(DelayTime);
        }

        protected override void OnAlgorithmInterrupted(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmInterrupted(sender, e);
            if (mainViewModel is MainWindowViewModel mainModel)
            {
                mainModel.IsPathfindingStarted = false;
            }
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
    }
}

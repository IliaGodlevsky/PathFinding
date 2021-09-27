﻿using Algorithm.Infrastructure.EventArguments;
using Common.Interface;
using GraphLib.Base;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Logging.Interface;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFVersion.Infrastructure;

namespace WPFVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel, IModel, IViewModel
    {
        public event EventHandler WindowClosed;

        public ICommand ConfirmPathFindAlgorithmChoice { get; }
        public ICommand CancelPathFindAlgorithmChoice { get; }

        public PathFindingViewModel(ILog log, IMainModel model, BaseEndPoints endPoints)
            : base(log, model, endPoints)
        {
            ConfirmPathFindAlgorithmChoice = new RelayCommand(ExecuteConfirmPathFindAlgorithmChoice,
                CanExecuteConfirmPathFindAlgorithmChoice);
            CancelPathFindAlgorithmChoice = new RelayCommand(ExecuteCloseWindowCommand);
        }

        private void InterruptAlgorithm(object sender, EventArgs e)
        {
            algorithm.Interrupt();
        }

        protected override void OnAlgorithmStarted(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmStarted(sender, e);
            if (mainViewModel is MainWindowViewModel mainModel)
            {
                mainModel.IsAlgorithmStarted = true;
                mainModel.AlgorithmInterrupted += InterruptAlgorithm;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    mainModel.Statistics.Add(string.Empty);
                    statIndex = mainModel.Statistics.Count - 1;
                });
            }
        }

        protected override void Summarize()
        {
            if (mainViewModel is MainWindowViewModel mainModel)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    mainModel.Statistics[statIndex]
                        = path.PathLength > 0 ? GetStatistics() : CouldntFindPathMsg;
                });
            }
        }

        protected override async void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            Thread.Sleep(DelayTime);
            await Task.Run(() =>
            {
                base.OnVertexVisited(sender, e);
                if (mainViewModel is MainWindowViewModel mainModel)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        mainModel.Statistics[statIndex] = GetStatistics();
                    });
                }
            });
        }

        protected override async void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            await Task.Run(() => base.OnVertexEnqueued(sender, e));
        }

        protected override void OnAlgorithmFinished(object sender, ProcessEventArgs e)
        {
            base.OnAlgorithmFinished(sender, e);
            if (mainViewModel is MainWindowViewModel mainModel)
            {
                mainModel.IsAlgorithmStarted = false;
                mainModel.AlgorithmInterrupted -= InterruptAlgorithm;
            }
        }

        private void ExecuteCloseWindowCommand(object param)
        {
            WindowClosed?.Invoke(this, EventArgs.Empty);
            WindowClosed = null;
        }

        private void ExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            ExecuteCloseWindowCommand(null);
            base.FindPath();
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            return Algorithms.Any(item => item.Item2 == Algorithm);
        }

        private int statIndex;
    }
}
using Algorithm.Extensions;
using Algorithm.Infrastructure.EventArguments;
using Common.Interface;
using GraphLib.Base;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WPFVersion.Infrastructure;

namespace WPFVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel, IViewModel, INotifyPropertyChanged
    {
        public event EventHandler OnWindowClosed;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand ConfirmPathFindAlgorithmChoice { get; }
        public ICommand CancelPathFindAlgorithmChoice { get; }

        public PathFindingViewModel(ILog log, IMainModel model, BaseEndPoints endPoints)
            : base(log, model, endPoints)
        {
            ConfirmPathFindAlgorithmChoice = new RelayCommand(
                ExecuteConfirmPathFindAlgorithmChoice,
                CanExecuteConfirmPathFindAlgorithmChoice);

            CancelPathFindAlgorithmChoice = new RelayCommand(ExecuteCloseWindowCommand);
        }

        protected override void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            base.OnVertexVisited(sender, e);
            if (mainViewModel is MainWindowViewModel mainModel)
            {
                mainModel.PathfindingsStatistics[statIndex] = GetStatistics();
            }
        }

        private void InterruptAlgorithm(object sender, EventArgs e)
        {
            algorithm.Interrupt();
        }

        protected override void OnAlgorithmStarted(object sender, AlgorithmEventArgs e)
        {
            base.OnAlgorithmStarted(sender, e);
            if (mainViewModel is MainWindowViewModel mainModel)
            {
                mainModel.CanInterruptAlgorithm = true;
                mainModel.OnAlgorithmInterrupted += InterruptAlgorithm;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    mainModel?.PathfindingsStatistics.Add(string.Empty);
                });
                mainModel?.PathfindingsStatistics.Add(string.Empty);
                statIndex = mainModel.PathfindingsStatistics.Count - 1;
            }
        }

        protected override void OnAlgorithmFinished(object sender, AlgorithmEventArgs e)
        {
            base.OnAlgorithmFinished(sender, e);
            if (mainViewModel is MainWindowViewModel mainModel)
            {
                mainModel.CanInterruptAlgorithm = false;
                mainModel.OnAlgorithmInterrupted -= InterruptAlgorithm;
            }
        }

        protected override void Summarize()
        {
            path.Highlight(endPoints);
            if (mainViewModel is MainWindowViewModel mainModel)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    mainModel.PathfindingsStatistics[statIndex] =
                    path.PathLength > 0 ? GetStatistics() : "Couldn't find path";
                });               
            }            
        }

        private void ExecuteCloseWindowCommand(object param)
        {
            OnWindowClosed?.Invoke(this, EventArgs.Empty);
            OnWindowClosed = null;
        }

        private void ExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            ExecuteCloseWindowCommand(null);
            base.FindPath();
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            return Algorithms.Values.Contains(Algorithm);
        }

        private int statIndex;
    }
}

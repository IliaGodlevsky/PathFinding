using Algorithm.Infrastructure.EventArguments;
using Common.Interface;
using GraphLib.Base;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFVersion3D.Infrastructure;

namespace WPFVersion3D.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel, IViewModel, INotifyPropertyChanged
    {
        public event EventHandler WindowClosed;
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

        protected override void OnAlgorithmStarted(object sender, AlgorithmEventArgs e)
        {
            base.OnAlgorithmStarted(sender, e);
            if (mainViewModel is MainWindowViewModel model)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    model.Statistics.Add(string.Empty);
                    statIndex = model.Statistics.Count - 1;
                });
            }
        }

        protected override async void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            Thread.Sleep(DelayTime);
            await Task.Run(() => base.OnVertexVisited(sender, e));
            if (mainViewModel is MainWindowViewModel model)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    model.Statistics[statIndex] = GetStatistics();
                });
            }
        }

        protected override async void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            await Task.Run(() => base.OnVertexEnqueued(sender, e));
        }

        protected override void Summarize()
        {
            if (mainViewModel is MainWindowViewModel model)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    model.Statistics[statIndex] = path.PathLength > 0
                        ? GetStatistics() : CouldntFindPathMsg;
                });
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
            return Algorithms.Values.Contains(Algorithm);
        }

        private int statIndex;
    }
}

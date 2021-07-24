using Algorithm.Infrastructure.EventArguments;
using Common.Interface;
using GraphLib.Base;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;
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

        protected override void ColorizeProcessedVertices(object sender, AlgorithmEventArgs e)
        {
            var frame = new DispatcherFrame();

            var callback = new DispatcherOperationCallback(arg =>
            {
                ((DispatcherFrame)arg).Continue = false;
                return null;
            });

            var priority = DispatcherPriority.Background;

            Dispatcher.CurrentDispatcher.BeginInvoke(priority, callback, frame);
            Dispatcher.PushFrame(frame);
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
    }
}

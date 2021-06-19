using AssembleClassesLib.EventArguments;
using AssembleClassesLib.Interface;
using Common.Interface;
using GraphLib.Base;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WPFVersion3D.Infrastructure;

namespace WPFVersion3D.ViewModel
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

        private IList<string> algorithmKeys;
        public override IList<string> AlgorithmKeys
        {
            get => algorithmKeys;
            set
            {
                algorithmKeys = value;
                OnPropertyChanged();
            }
        }

        public PathFindingViewModel(ILog log, IAssembleClasses pluginsLoader,
            IMainModel model, BaseEndPoints endPoints)
            : base(log, pluginsLoader, model, endPoints)
        {
            ConfirmPathFindAlgorithmChoice = new RelayCommand(
                ExecuteConfirmPathFindAlgorithmChoice,
                CanExecuteConfirmPathFindAlgorithmChoice);

            CancelPathFindAlgorithmChoice = new RelayCommand(ExecuteCloseWindowCommand);
        }

        public override void UpdateAlgorithmKeys(object sender, AssembleClassesEventArgs e)
        {
            Application.Current?.Dispatcher?.Invoke(() =>
            {
                base.UpdateAlgorithmKeys(sender, e);
            });
        }

        protected override void ColorizeProcessedVertices(object sender, EventArgs e)
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
            return AlgorithmKeys.Contains(AlgorithmKey);
        }
    }
}

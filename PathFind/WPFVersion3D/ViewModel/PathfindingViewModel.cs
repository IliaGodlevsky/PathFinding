using Algorithm.AlgorithmCreating;
using Common.Interfaces;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;
using System.Linq;
using System.Windows.Input;
using WPFVersion3D.Infrastructure;

namespace WPFVersion3D.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel, IViewModel
    {
        public event EventHandler OnWindowClosed;

        public ICommand ConfirmPathFindAlgorithmChoice { get; }
        public ICommand CancelPathFindAlgorithmChoice { get; }

        public PathFindingViewModel(IMainModel model) : base(model)
        {
            ConfirmPathFindAlgorithmChoice = new RelayCommand(
                ExecuteConfirmPathFindAlgorithmChoice,
                CanExecuteConfirmPathFindAlgorithmChoice);
            CancelPathFindAlgorithmChoice = new RelayCommand(obj => CloseWindow());
            pauseProvider.PauseEvent += () => System.Windows.Forms.Application.DoEvents();
        }

        private void CloseWindow()
        {
            OnWindowClosed?.Invoke(this, new EventArgs());
        }

        private void ExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            CloseWindow();
            base.FindPath();
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            return AlgorithmFactory.AlgorithmsDescriptions.Any(algo => algo == AlgorithmKey);
        }
    }
}

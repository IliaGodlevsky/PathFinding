using Algorithm.Realizations;
using Common.Interface;
using GraphViewModel;
using GraphViewModel.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel, IViewModel
    {
        public event EventHandler OnWindowClosed;

        public PathFindingViewModel(IMainModel model) : base(model)
        {
            AlgorithmKeys = AlgorithmsFactory.AlgorithmsDescriptions.ToList();
        }

        public void StartPathfinding(object sender, EventArgs e)
        {
            if (CanExecuteConfirmPathFindAlgorithmChoice())
            {
                OnWindowClosed?.Invoke(this, EventArgs.Empty);
                FindPath();
            }
        }

        protected override void ColorizeProcessedVertices()
        {
            Application.DoEvents();
        }

        public void CancelPathFinding(object sender, EventArgs e)
        {
            OnWindowClosed?.Invoke(this, EventArgs.Empty);
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice()
        {
            return AlgorithmKeys.Contains(AlgorithmKey);
        }
    }
}

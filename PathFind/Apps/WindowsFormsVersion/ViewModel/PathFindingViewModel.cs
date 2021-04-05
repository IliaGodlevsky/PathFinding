using Algorithm.Realizations;
using Common.Interface;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;
using GraphViewModel;

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
                var args = new EventArgs();
                OnWindowClosed?.Invoke(this, args);
                FindPath();
                OnWindowClosed = null;
            }
        }

        protected override void OnAlgorithmIntermitted()
        {
            Application.DoEvents();
        }

        public void CancelPathFinding(object sender, EventArgs e)
        {
            var args = new EventArgs();
            OnWindowClosed?.Invoke(this, args);
            OnWindowClosed = null;
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice()
        {
            return AlgorithmKeys.Contains(AlgorithmKey);
        }
    }
}

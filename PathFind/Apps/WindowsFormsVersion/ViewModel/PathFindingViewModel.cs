using Algorithm.Realizations;
using Common.Interface;
using GraphLib.ViewModel;
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

        public void PathFind(object sender, EventArgs e)
        {
            if (CanExecuteConfirmPathFindAlgorithmChoice())
            {
                OnWindowClosed?.Invoke(this, new EventArgs());
                FindPath();
            }
            OnWindowClosed = null;
        }

        protected override void OnAlgorithmIntermitted()
        {
            Application.DoEvents();
        }

        public void CancelPathFind(object sender, EventArgs e)
        {
            OnWindowClosed?.Invoke(this, new EventArgs());
            OnWindowClosed = null;
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice()
        {
            return AlgorithmKeys.Any(algo => algo == AlgorithmKey);
        }
    }
}

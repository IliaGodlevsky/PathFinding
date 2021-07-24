using Algorithm.Infrastructure.EventArguments;
using Common.Interface;
using GraphLib.Base;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.Windows.Forms;

namespace WindowsFormsVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel, IViewModel
    {
        public event EventHandler OnWindowClosed;

        public PathFindingViewModel(ILog log, IMainModel model, BaseEndPoints endPoints)
            : base(log, model, endPoints)
        {

        }

        public void StartPathfinding(object sender, EventArgs e)
        {
            if (CanExecuteConfirmPathFindAlgorithmChoice())
            {
                OnWindowClosed?.Invoke(this, EventArgs.Empty);
                OnWindowClosed = null;
                FindPath();
            }
        }

        protected override void ColorizeProcessedVertices(object sender, AlgorithmEventArgs e)
        {
            Application.DoEvents();
        }

        public void CancelPathFinding(object sender, EventArgs e)
        {
            OnWindowClosed?.Invoke(this, EventArgs.Empty);
            OnWindowClosed = null;
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice()
        {
            return Algorithms.Values.Contains(Algorithm);
        }
    }
}

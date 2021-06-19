using AssembleClassesLib.Interface;
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

        public PathFindingViewModel(ILog log, IAssembleClasses pluginsLoader,
            IMainModel model, BaseEndPoints endPoints)
            : base(log, pluginsLoader, model, endPoints)
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

        protected override void ColorizeProcessedVertices(object sender, EventArgs e)
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
            return AlgorithmKeys.Contains(AlgorithmKey);
        }
    }
}

using AssembleClassesLib.Interface;
using Common.Interface;
using GraphViewModel;
using GraphViewModel.Interfaces;
using System;
using System.Windows.Forms;

namespace WindowsFormsVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel, IViewModel
    {
        public event EventHandler OnWindowClosed;

        public PathFindingViewModel(IAssembleClasses pluginsLoader, IMainModel model)
            : base(pluginsLoader, model)
        {

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

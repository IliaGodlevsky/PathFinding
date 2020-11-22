using Algorithm.AlgorithmCreating;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel
    {
        public PathFindingViewModel(IMainModel model) : base(model)
        {
            pauseProvider.PauseEvent += () => Application.DoEvents();
        }

        public void PathFind(object sender, EventArgs e)
        {
            if (CanExecuteConfirmPathFindAlgorithmChoice())
            {
                (mainViewModel as MainWindowViewModel).Window.Close();
                FindPath();
            }
        }

        public void CancelPathFind(object sender, EventArgs e)
        {           
            (mainViewModel as MainWindowViewModel).Window.Close();
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice()
        {
            return AlgorithmFactory.AlgorithmKeys.Any(algo => algo == AlgorithmKey);
        }
    }
}

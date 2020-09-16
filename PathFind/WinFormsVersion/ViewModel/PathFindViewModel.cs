using GraphLibrary.Enums;
using GraphLibrary.PauseMaker;
using GraphLibrary.ViewModel;
using GraphLibrary.ViewModel.Interface;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsVersion.ViewModel
{
    internal class PathFindViewModel : AbstractPathFindModel
    {
        public PathFindViewModel(IMainModel model) : base(model)
        {

        }

        protected override void PrepareAlgorithm()
        {
            (mainViewModel as MainWindowViewModel).Window.Close();
            var pauser = new PauseProvider(DelayTime);
            pauser.PauseEvent += () => Application.DoEvents();
            pathAlgorithm.OnVertexVisited += (vertex) => pauser.Pause();

            pathAlgorithm.OnFinished += (sender, eventArgs) =>
            {
                if (!eventArgs.HasFoundPath)
                    MessageBox.Show(badResultMessage);
            };
            base.PrepareAlgorithm();
        }

        public void PathFind(object sender, EventArgs e)
        {
            if (CanExecuteConfirmPathFindAlgorithmChoice())            
                FindPath();
        }

        public void CancelPathFind(object sender, EventArgs e)
        {           
            (mainViewModel as MainWindowViewModel).Window.Close();
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice()
        {
            return (Enum.GetValues(typeof(Algorithms)) as Algorithms[]).Any(algo => algo == Algorithm);
        }
    }
}

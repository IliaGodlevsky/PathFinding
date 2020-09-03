using GraphLibrary.AlgorithmEnum;
using GraphLibrary.Model;
using GraphLibrary.PauseMaker;
using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsVersion.Model;

namespace WinFormsVersion.ViewModel
{
    internal class PathFindViewModel : AbstractPathFindModel
    {
        public PathFindViewModel(IMainModel model) : base(model)
        {
            filler = new WinFormsGraphFiller();
        }

        protected override void PrepareAlgorithm()
        {
            (model as MainWindowViewModel).Window.Close();
            pathAlgorithm.Pauser = new PauseProvider() { PauseEvent = () => Application.DoEvents() };
        }

        protected override void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void PathFind(object sender, EventArgs e)
        {
            if (CanExecuteConfirmPathFindAlgorithmChoice())            
                PathFind();
        }

        public void CancelPathFind(object sender, EventArgs e)
        {
            (model as MainWindowViewModel).Window.Close();
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice()
        {
            return (Enum.GetValues(typeof(Algorithms)) as Algorithms[]).Any(algo => algo == Algorithm);
        }
    }
}

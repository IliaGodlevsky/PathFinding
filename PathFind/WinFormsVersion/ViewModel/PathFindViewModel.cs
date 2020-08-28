using GraphLibrary.AlgorithmEnum;
using GraphLibrary.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsVersion.ViewModel
{
    public class PathFindViewModel : AbstractPathFindModel
    {
        public PathFindViewModel(IMainModel model) : base(model)
        {

        }

        protected override void FindPreparations()
        {
            (model as MainWindowViewModel).Window.Close();
            pathAlgorithm.PauseEvent = () => Application.DoEvents();
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

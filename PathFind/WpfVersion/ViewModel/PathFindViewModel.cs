using GraphLibrary.AlgorithmEnum;
using GraphLibrary.Model;
using GraphLibrary.PauseMaker;
using System;
using System.Linq;
using System.Windows;
using WpfVersion.Infrastructure;

namespace WpfVersion.ViewModel
{
    internal class PathFindViewModel : AbstractPathFindModel
    {
        public RelayCommand ConfirmPathFindAlgorithmChoice { get; }
        public RelayCommand CancelPathFindAlgorithmChoice { get; }

        public PathFindViewModel(IMainModel model) : base(model)
        {
            this.model = model;
            graph = model.Graph;

            ConfirmPathFindAlgorithmChoice = new RelayCommand(
                ExecuteConfirmPathFindAlgorithmChoice,
                CanExecuteConfirmPathFindAlgorithmChoice);

            CancelPathFindAlgorithmChoice = new RelayCommand(obj => 
            (model as MainWindowViewModel)?.Window.Close(), obj => true);
        }

        private void ExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            base.PathFind();
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            return (Enum.GetValues(typeof(Algorithms)) as Algorithms[]).Any(algo => algo == Algorithm);
        }

        protected override void PrepareAlgorithm()
        {
            (model as MainWindowViewModel).Window.Close();
            pathAlgorithm.Pauser = new PauseProvider() { PauseEvent = () => System.Windows.Forms.Application.DoEvents() };
        }

        protected override void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}

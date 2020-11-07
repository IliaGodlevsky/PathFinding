using Algorithm.AlgorithmCreating;
using GraphLib.PauseMaking;
using GraphLib.ViewModel;
using GraphViewModel.Interfaces;
using System.Linq;
using System.Windows;
using WpfVersion.Infrastructure;

namespace WpfVersion.ViewModel
{
    internal class PathFindingViewModel : PathFindingModel
    {
        public RelayCommand ConfirmPathFindAlgorithmChoice { get; }
        public RelayCommand CancelPathFindAlgorithmChoice { get; }

        public PathFindingViewModel(IMainModel model) : base(model)
        {
            ConfirmPathFindAlgorithmChoice = new RelayCommand(
                ExecuteConfirmPathFindAlgorithmChoice,
                CanExecuteConfirmPathFindAlgorithmChoice);

            CancelPathFindAlgorithmChoice = new RelayCommand(obj => 
            (model as MainWindowViewModel)?.Window.Close(), obj => true);
        }

        private void ExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            base.FindPath();
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            return AlgorithmFactory.AlgorithmKeys.Any(algo => algo == AlgorithmKey);
        }

        protected override void PrepareAlgorithm()
        {
            base.PrepareAlgorithm();

            (mainViewModel as MainWindowViewModel).Window.Close();

            var pauser = new PauseProvider(DelayTime);
            pauser.PauseEvent += () => System.Windows.Forms.Application.DoEvents();

            pathAlgorithm.OnVertexVisited += (vertex) => pauser.Pause();  
            
            pathAlgorithm.OnFinished += (sender, eventArgs) =>
            {
                if (!eventArgs.HasFoundPath)
                    MessageBox.Show(badResultMessage);                
            };
        }
    }
}

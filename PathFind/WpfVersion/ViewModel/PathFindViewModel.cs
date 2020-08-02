using GraphLibrary.Algorithm;
using GraphLibrary.Enums.AlgorithmEnum;
using GraphLibrary.PathFindAlgorithmSelector;
using System;
using System.Linq;
using System.Windows;
using WpfVersion.Infrastructure;
using WpfVersion.Model.Graph;
using WpfVersion.ViewModel.Resources;

namespace WpfVersion.ViewModel
{
    public class PathFindViewModel : BaseViewModel
    {
        private IPathFindAlgorithm pathFindAlgorythm;
        public Algorithms Algorithm { get; set; }

        private WpfGraph graph;
        private MainWindowViewModel model;

        public RelayCommand ConfirmPathFindAlgorithmChoice { get; }
        public RelayCommand CancelPathFindAlgorithmChoice { get; }

        public PathFindViewModel(MainWindowViewModel model)
        {
            this.model = model;
            graph = model.Graph;

            ConfirmPathFindAlgorithmChoice = new RelayCommand(
                ExecuteConfirmPathFindAlgorithmChoice,
                CanExecuteConfirmPathFindAlgorithmChoice);

            CancelPathFindAlgorithmChoice = new RelayCommand(obj => model?.Window.Close(), obj => true);
        }

        private void ExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            pathFindAlgorythm = AlgorithmSelector.GetPathFindAlgorithm(Algorithm, model.Graph);
            model.Window.Close();
            pathFindAlgorythm.PauseEvent = () => System.Windows.Forms.Application.DoEvents();
            if (pathFindAlgorythm.FindDestionation())
            {
                pathFindAlgorythm.DrawPath();
                model.Statistics = pathFindAlgorythm.StatCollector.GetStatistics().GetFormattedData();
                graph.Start = null;
                graph.End = null;
            }
            else
                MessageBox.Show(ViewModelResources.BadResultMessage);
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            return (Enum.GetValues(typeof(Algorithms)) as Algorithms[]).Any(algo => algo == Algorithm);
        }
    }
}

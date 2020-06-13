using GraphLibrary.Algorithm;
using GraphLibrary.Enums.AlgorithmEnum;
using GraphLibrary.PathFindAlgorithmSelector;
using System.Windows;
using WpfVersion.Infrastructure;
using WpfVersion.Model.Graph;
using WpfVersion.Model.PauseMaker;

namespace WpfVersion.ViewModel
{
    public class PathFindViewModel
    {
        private IPathFindAlgorithm pathFindAlgorythm;
        private WpfGraph graph;
        private Algorithms algorithm;
        private MainWindowViewModel model;
        public RelayCommand ConfirmPathFindAlgorithmChoice { get; }
        public RelayCommand CancelPathFindAlgorithmChoice { get; }
        public RelayCommand ChooseDijkstraAlgorithmCommand { get; }
        public RelayCommand ChooseAStartAlgorithmCommand { get; }
        public RelayCommand ChooseDeepPathFindAlgorithmCommand { get; }
        public RelayCommand ChooseWidePathFindAlgorithmCommand { get; }

        public PathFindViewModel(MainWindowViewModel model)
        {
            this.model = model;
            graph = model.Graph;
            ConfirmPathFindAlgorithmChoice = new RelayCommand(ExecuteConfirmPathFindAlgorithmChoice,
                CanExecuteConfirmPathFindAlgorithmChoice);
            CancelPathFindAlgorithmChoice = new RelayCommand(obj => model?.Window.Close(), obj => true);
            ChooseDijkstraAlgorithmCommand = new RelayCommand(obj => algorithm = Algorithms.DijkstraAlgorithm, obj => true);
            ChooseAStartAlgorithmCommand = new RelayCommand(obj => algorithm = Algorithms.AStarAlgorithm, obj => true);
            ChooseDeepPathFindAlgorithmCommand = new RelayCommand(obj => algorithm = Algorithms.DeepPathFind, obj => true);
            ChooseWidePathFindAlgorithmCommand = new RelayCommand(obj => algorithm = Algorithms.WidePathFind, obj => true);
        }

        private void ExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            pathFindAlgorythm = AlgorithmSelector.GetPathFindAlgorithm(algorithm, model.Graph);
            model.Window.Close();
            pathFindAlgorythm.Pause = new WpfPauseMaker().Pause;
            if (pathFindAlgorythm.FindDestionation())
            {
                pathFindAlgorythm.DrawPath();
                model.Statistics = pathFindAlgorythm.StatCollector.Statistics;
                graph.Start = null;
                graph.End = null;
            }
            else
                MessageBox.Show("Couldn't find path");
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            return algorithm == Algorithms.DijkstraAlgorithm ||
                algorithm == Algorithms.AStarAlgorithm ||
                algorithm == Algorithms.DeepPathFind ||
                algorithm == Algorithms.WidePathFind;
        }
    }
}

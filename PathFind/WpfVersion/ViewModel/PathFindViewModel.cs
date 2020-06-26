using GraphLibrary.Algorithm;
using GraphLibrary.Enums.AlgorithmEnum;
using GraphLibrary.PathFindAlgorithmSelector;
using GraphLibrary.Statistics;
using System;
using System.Windows;
using WpfVersion.Extensions.ArrayExtension;
using WpfVersion.Infrastructure;
using WpfVersion.Model.Graph;
using WpfVersion.Model.PauseMaker;

namespace WpfVersion.ViewModel
{
    public class PathFindViewModel : BaseViewModel
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
        public RelayCommand ChooseValueGreedPathFindAlgorithmCommand { get; }
        public RelayCommand ChooseDistanceGreedPathFindAlgorithmCommand { get; }

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
            ChooseValueGreedPathFindAlgorithmCommand = new RelayCommand(obj => algorithm = Algorithms.ValueGreedyAlgorithm, obj => true);
            ChooseDistanceGreedPathFindAlgorithmCommand = new RelayCommand(obj => algorithm = Algorithms.DistanceGreedyAlgorithm, obj => true);
        }

        public string TransformStatistics(Statistics statistics)
        {
            return "Time: " + string.Format("{0}:{1}.{2}", statistics.Duration.Minute, statistics.Duration.Second, statistics.Duration.Millisecond)
                + " | Steps: " + statistics.Steps.ToString()
                + " | Path length: " + statistics.PathLength.ToString()
                + " | Visited vertices: " + statistics.VisitedVertices.ToString();
        }

        private void ExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            pathFindAlgorythm = AlgorithmSelector.GetPathFindAlgorithm(algorithm, model.Graph);
            model.Window.Close();
            pathFindAlgorythm.Pause = new WpfPauseMaker().Pause;
            if (pathFindAlgorythm.FindDestionation())
            {
                pathFindAlgorythm.DrawPath();
                model.Statistics = TransformStatistics(pathFindAlgorythm.StatCollector.GetStatistics());
                graph.Start = null;
                graph.End = null;
            }
            else
                MessageBox.Show("Couldn't find path");
        }

        private bool CanExecuteConfirmPathFindAlgorithmChoice(object param)
        {
            return Enum.GetValues(typeof(Algorithms)).Exists(algo => (Algorithms)algo == algorithm);
        }
    }
}

using GraphLibrary.Algorithm;
using GraphLibrary.Constants;
using GraphLibrary.Enums.AlgorithmEnum;
using GraphLibrary.GraphFactory;
using GraphLibrary.PathFindAlgorithmSelector;
using GraphLibrary.RoleChanger;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WpfVersion.Infrastructure;
using WpfVersion.Model.Graph;
using WpfVersion.Model.GraphFactory;
using WpfVersion.Model.PauseMaker;
using WpfVersion.Model.RoleChanger;
using WpfVersion.Model.Vertex;
using WpfVersion.View.Windows;

namespace WpfVersion.ViewModel
{
    public class WpfVersionViewModel : INotifyPropertyChanged
    {
        public Algorithms Algorithm { get; set; }

        public IPathFindAlgorithm Algorythm { get; set; }

        private string statistics;

        public string Statistics 
        { 
            get
            {
                return statistics;
            }
            private set
            {
                statistics = value; OnPropertyChanged();
            }
        }

        private IVertexRoleChanger changer;

        private Window window;

        public event PropertyChangedEventHandler PropertyChanged;

        public Canvas GraphField { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ObstaclePercent { get; set; }
        public WpfGraph Graph { get; set; }

        public RelayCommand StartPathFindCommand { get; }
        public RelayCommand CreateNewGraphCommand { get; }
        public RelayCommand ClearGraphCommand { get; }
        public RelayCommand ConfirmCreateGraphCommand { get; }
        public RelayCommand CancelCreateGrapgCommand { get; }
        public RelayCommand ChoosePathFindAlgorythmCommand { get; }
        public RelayCommand ConfirmChoosePathFindAlgorythmCommand { get; }
        public WpfVersionViewModel()
        {
            GraphField = new Canvas();

            StartPathFindCommand = new RelayCommand(ExecuteStartPathFindCommand, 
                CanExecuteStartPathFindCommand);

            CreateNewGraphCommand = new RelayCommand(ExecuteCreateNewGraphCommand, 
                CanExecuteCreateGraphCommand);

            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, 
                CanExecuteClearGraphCommand);

            ConfirmCreateGraphCommand = new RelayCommand(ExecuteConfirmCreateGraphCommand, 
                CanExecuteConfirmCreateGraphCommand);

            CancelCreateGrapgCommand = new RelayCommand(ExecuteCancelCreateGraphCommand, 
                CanExecuteCancelCommand);

            ConfirmChoosePathFindAlgorythmCommand = new RelayCommand(ExecuteConfirmChoosePathFindAlgorithm,
                CanExecuteConfirmChoosePathFindAlorithm);

        }

        private void FillGraphField()
        {
            GraphField.Children.Clear();
            for (int i = 0; i < Graph.Width; i++) 
            {
                for (int j = 0; j < Graph.Height; j++) 
                {
                    GraphField.Children.Add(Graph[i,j] as WpfVertex);
                    if (!Graph[i, j].IsObstacle)
                        (Graph[i, j] as WpfVertex).Click += changer.SetStartPoint;
                    (Graph[i, j] as WpfVertex).MouseRightButtonDown += changer.ReversePolarity;
                    Canvas.SetLeft(Graph[i, j] as WpfVertex, Const.SIZE_BETWEEN_VERTICES * i);
                    Canvas.SetTop(Graph[i, j] as WpfVertex, Const.SIZE_BETWEEN_VERTICES * j);
                }
            }
        }

        private void ExecuteConfirmChoosePathFindAlgorithm(object param)
        {
            Algorythm = AlgorithmSelector.GetPathFindAlgorithm(Algorithm, Graph);
            Algorythm.Pause = new WpfPauseMaker().Pause;
            window.Close();
            if (Algorythm.FindDestionation())
            {
                Algorythm.DrawPath();
                Statistics = Algorythm.StatCollector.Statistics;
                Graph.Start = null;
                Graph.End = null;
            }
        }

        private void ExecuteClearGraphCommand(object param)
        {
            Graph.Refresh();
            Statistics = string.Empty;
        }

        private bool CanExecuteClearGraphCommand(object param)
        {
            return Graph != null;
        }

        private void ExecuteStartPathFindCommand(object param)
        {
            window = new PathFindParametresWindow();
            window.DataContext = this;
            window.Show();
        }

        private bool CanExecuteStartPathFindCommand(object param)
        {
            return Graph?.End != null && Graph?.Start != null;
        }

        private void ExecuteCreateNewGraphCommand(object param)
        {
            window = new GraphParametresWindow();
            window.DataContext = this;
            window.Show();
        }

        private void ExecuteConfirmCreateGraphCommand(object param)
        {
            IGraphFactory factory =
                new RandomValuedWpfGraphFactory(ObstaclePercent, Width, Height, Const.SIZE_BETWEEN_VERTICES);
            Graph = (WpfGraph)factory.GetGraph();
            changer = new WpfRoleChanger(Graph);
            Graph.SetEnd += changer.SetDestinationPoint;
            Graph.SetStart += changer.SetStartPoint;
            FillGraphField();
            window.Close();
        }

        private void ExecuteCancelCreateGraphCommand(object param)
        {
            window.Close();
        }

        private bool CanExecuteConfirmCreateGraphCommand(object param)
        {
            return Width != 0 && Height != 0;
        }

        private bool CanExecuteCreateGraphCommand(object param)
        {
            return true;
        }

        private bool CanExecuteCancelCommand(object param)
        {
            return true;
        }

        private bool CanExecuteConfirmChoosePathFindAlorithm(object param)
        {
            return Algorithm == Algorithms.DijkstraAlgorithm ||
                Algorithm == Algorithms.AStarAlgorithm ||
                Algorithm == Algorithms.DeepPathFind ||
                Algorithm == Algorithms.WidePathFind;
        }

        public virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }
    }
}

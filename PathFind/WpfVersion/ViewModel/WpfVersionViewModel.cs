using GraphLibrary.Algorithm;
using GraphLibrary.Constants;
using GraphLibrary.GraphFactory;
using GraphLibrary.RoleChanger;
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
    public class WpfVersionViewModel
    {
        private IPathFindAlgorithm algorythm;
        private IVertexRoleChanger changer;
        public Canvas GraphField { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ObstaclePercent { get; set; }
        public WpfGraph Graph { get; set; }

        public RelayCommand QuickStartPathFindCommand { get; }
        public RelayCommand CreateNewGraphCommand { get; }
        public RelayCommand ClearGraphCommand { get; }
        public RelayCommand OkCommand { get; set; }
        public WpfVersionViewModel()
        {
            GraphField = new Canvas();
            QuickStartPathFindCommand = new RelayCommand(ExecuteQuickStartPathFindCommand, 
                CanExecuteQuickStartPathFindCommand);
            CreateNewGraphCommand = new RelayCommand(ExecuteCreateNewGraphCommand, 
                CanExecuteCreateGraphCommand);
            ClearGraphCommand = new RelayCommand(ExecuteClearGraphCommand, 
                CanExecuteClearGraphCommand);
            OkCommand = new RelayCommand(ExecuteOkCommand, CanExecuteOkCommand);
            Button button = new Button();
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

        private void ExecuteClearGraphCommand(object param)
        {
            Graph.Refresh();
        }

        private bool CanExecuteClearGraphCommand(object param)
        {
            return true;
        }

        private void ExecuteQuickStartPathFindCommand(object param)
        {
            algorythm = new DijkstraAlgorithm(Graph);
            algorythm.Pause = new WpfPauseMaker().Pause;
            if (algorythm.FindDestionation())
            {
                algorythm.DrawPath();
                Graph.Start = null;
                Graph.End = null;
            }
        }

        private bool CanExecuteQuickStartPathFindCommand(object param)
        {
            return true;
        }

        private void ExecuteCreateNewGraphCommand(object param)
        {
            GraphParametresWindow window = new GraphParametresWindow();
            window.DataContext = this;
            window.Show();
        }

        private void ExecuteOkCommand(object param)
        {
            IGraphFactory factory = new RandomValuedWpfGraphFactory(ObstaclePercent, Width, Height);
            Graph = (WpfGraph)factory.GetGraph();
            changer = new WpfRoleChanger(Graph);
            Graph.SetEnd += changer.SetDestinationPoint;
            Graph.SetStart += changer.SetStartPoint;
            FillGraphField();
        }

        private bool CanExecuteOkCommand(object param)
        {
            return Width != 0 && Height != 0 && ObstaclePercent != 0;
        }

        private bool CanExecuteCreateGraphCommand(object param)
        {
            return true;
        }
    }
}

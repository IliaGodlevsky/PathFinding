using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using System.Drawing;
using Console = Colorful.Console;
using GraphLib.Graphs;
using GraphViewModel;

namespace ConsoleVersion.ViewModel
{
    internal class MainViewModel : MainModel
    {
        public MainViewModel() : base()
        {            
            VertexEventHolder = new ConsoleVertexEventHolder();
            FieldFactory = new ConsoleGraphFieldFactory();
            InfoConverter = (info) => new ConsoleVertex(info);
        }

        public override void CreateNewGraph()
        {
            var model = new GraphCreatingViewModel(this);
            var view = new GraphCreateView(model);

            view.Start();
        }

        public override void FindPath()
        {
            var model = new PathFindingViewModel(this);
            var view = new PathFindView(model);

            view.Start();
        }

        public void ChangeStatus()
        {
            var upperPossibleXValue = (Graph as Graph2D).Width;
            var upperPossibleYValue = (Graph as Graph2D).Length;

            var point = Input.InputPoint(upperPossibleXValue, upperPossibleYValue);

            (Graph[point] as ConsoleVertex).ChangeStatus();
        }

        public void ChangeVertexCost()
        {
            var upperPossibleXValue = (Graph as Graph2D).Width;
            var upperPossibleYValue = (Graph as Graph2D).Length;

            var point = Input.InputPoint(upperPossibleXValue, upperPossibleYValue);

            while (Graph[point].IsObstacle)
            {
                point = Input.InputPoint(upperPossibleXValue, upperPossibleYValue);
            }

            (Graph[point] as ConsoleVertex).ChangeCost();
        }

        protected override string GetSavingPath()
        {
            return GetPath();
        }

        protected override string GetLoadingPath()
        {
            return GetPath();
        }

        private string GetPath()
        {
            Console.Write("Enter path: ");
            return Console.ReadLine();
        }

        public void DisplayGraph()
        {
            Console.Clear();
            Console.ForegroundColor = Color.White;
            Console.WriteLine(GraphParametres);
            (GraphField as ConsoleGraphField)?.ShowGraphWithFrames();
            Console.WriteLine(PathFindingStatistics);
        }
    }
}

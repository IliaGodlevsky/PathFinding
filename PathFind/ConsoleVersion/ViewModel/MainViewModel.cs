using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using System.Drawing;
using Console = Colorful.Console;
using GraphLib.Graphs;
using GraphViewModel;
using System;
using Common.EventArguments;

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
            model.OnPathNotFound += OnPathNotFound;
            var view = new PathFindView(model);

            view.Start();
        }

        public void ReverseVertex()
        {
            var upperPossibleXValue = (Graph as Graph2D).Width - 1;
            var upperPossibleYValue = (Graph as Graph2D).Length - 1;

            var point = Input.InputPoint(upperPossibleXValue, upperPossibleYValue);

            (Graph[point] as ConsoleVertex).Reverse();
        }

        public void ChangeVertexCost()
        {
            var graph2D = Graph as Graph2D;
            var upperPossibleXValue = graph2D.Width - 1;
            var upperPossibleYValue = graph2D.Length - 1;

            var point = Input.InputPoint(upperPossibleXValue, upperPossibleYValue);

            while (Graph[point].IsObstacle)
            {
                point = Input.InputPoint(upperPossibleXValue, upperPossibleYValue);
            }

            (Graph[point] as ConsoleVertex).ChangeCost();
        }

        public void DisplayGraph()
        {
            Console.Clear();
            Console.ForegroundColor = Color.White;
            Console.WriteLine(GraphParametres);
            var field = GraphField as ConsoleGraphField;
            field?.ShowGraphWithFrames();
            Console.WriteLine(PathFindingStatistics);
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

        private void OnPathNotFound(object sender, EventArgs e)
        {
            var args = e as PathNotFoundEventArgs;

            DisplayGraph();
            Console.WriteLine(args.Message);
            Console.ReadLine();
        }
    }
}

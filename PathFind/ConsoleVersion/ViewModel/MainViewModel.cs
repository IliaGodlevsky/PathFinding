using Common;
using ConsoleVersion.Attributes;
using ConsoleVersion.Enums;
using ConsoleVersion.InputClass;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Graphs;
using GraphLib.Interface;
using GraphLib.NullObjects;
using GraphLib.VertexCost;
using GraphViewModel;
using System;
using System.Drawing;
using System.Linq;
using Console = Colorful.Console;

namespace ConsoleVersion.ViewModel
{
    internal class MainViewModel : MainModel
    {
        public MainViewModel(BaseGraphFieldFactory fieldFactory,
            IVertexEventHolder eventHolder,
            IGraphSerializer graphSerializer,
            IGraphAssembler graphFactory,
            IPathInput pathInput) : base(fieldFactory, eventHolder, graphSerializer, graphFactory, pathInput)
        {
            graphFactory.OnExceptionCaught += OnExceptionCaught;
            graphSerializer.OnExceptionCaught += OnExceptionCaught;
        }

        [MenuItem("Make unweighted")]
        public void MakeGraphUnweighted() => Graph.ToUnweighted();

        [MenuItem("Make weighted")]
        public void MakeGraphWeighted() => Graph.ToWeighted();

        [MenuItem("Create new graph", MenuItemPriority.Highest)]
        public override void CreateNewGraph()
        {
            try
            {
                var model = new GraphCreatingViewModel(this, graphAssembler);
                var view = new GraphCreateView(model);

                view.Start();
            }
            catch(Exception ex)
            {
                Logger.Instance.Error(ex);
            }
            
        }

        [MenuItem("Find path", MenuItemPriority.High)]
        public override void FindPath()
        {
            if (HasVerticesToChooseAsExtream())
            {
                try
                {
                    var model = new PathFindingViewModel(this)
                    {
                        EndPoints = EndPoints
                    };
                    model.OnPathNotFound += OnPathNotFound;
                    var view = new PathFindView(model);

                    view.Start();
                }
                catch (Exception ex)
                {
                    Logger.Instance.Error(ex);
                }
            }
            else
            {
                Console.WriteLine("No vertices to choose as extreme vertices");
                Console.ReadLine();
            }
        }

        [MenuItem("Reverse vertex")]
        public void ReverseVertex()
        {
            if (Graph.Any())
            {
                var graph2D = Graph as Graph2D;
                var upperPossibleXValue = graph2D.Width - 1;
                var upperPossibleYValue = graph2D.Length - 1;

                var point = Input.InputPoint(upperPossibleXValue, upperPossibleYValue);

                (Graph[point] as Vertex).Reverse();
            }
        }

        [MenuItem("Change cost range", MenuItemPriority.Low)]
        public void ChangeVertexCostValueRange()
        {
            string message = "Enter upper vertex cost value: ";
            var upperValueRange = Input.InputNumber(message, 99, 1);
            Cost.CostRange = new ValueRange(upperValueRange, 1);
        }

        [MenuItem("Change vertex cost", MenuItemPriority.Low)]
        public void ChangeVertexCost()
        {
            if (Graph.Any())
            {
                var graph2D = Graph as Graph2D;
                var upperPossibleXValue = graph2D.Width - 1;
                var upperPossibleYValue = graph2D.Length - 1;

                var point = Input.InputPoint(upperPossibleXValue, upperPossibleYValue);

                while (Graph[point].IsObstacle)
                {
                    point = Input.InputPoint(upperPossibleXValue, upperPossibleYValue);
                }

                (Graph[point] as Vertex).ChangeCost();
            }
        }

        [MenuItem("Save graph")]
        public override void SaveGraph() => base.SaveGraph();

        [MenuItem("Load graph")]
        public override void LoadGraph()
        {
            base.LoadGraph();
            MainView.UpdatePositionOfVisualElements(Graph);
        }

        [MenuItem("Quit programm", MenuItemPriority.Lowest)]
        public void Quit()
        {
            Environment.Exit(0);
        }

        public void DisplayGraph()
        {
            Console.Clear();
            Console.ForegroundColor = Color.White;
            Console.WriteLine(GraphParametres);
            var field = GraphField as GraphField;
            field?.ShowGraphWithFrames();
            Console.WriteLine(PathFindingStatistics);
            if (MainView.PathfindingStatisticsPosition is Coordinate2D position)
            {
                Console.SetCursorPosition(position.X, position.Y + 1);
            }
            Console.CursorVisible = true;
        }

        private void OnPathNotFound(string message)
        {
            DisplayGraph();
            Console.WriteLine(message);
            Console.ReadLine();
        }

        private void OnExceptionCaught(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadLine();
        }

        private bool HasVerticesToChooseAsExtream()
        {
            var verticesWithNeighboursCount = Graph.Count(vertex => vertex.Neighbours.Any());
            return verticesWithNeighboursCount >= 2;
        }
    }
}

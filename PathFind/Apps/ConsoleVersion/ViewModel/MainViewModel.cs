using Algorithm.Realizations;
using Common;
using Common.Logging;
using ConsoleVersion.Attributes;
using ConsoleVersion.Enums;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using GraphLib.Base;
using GraphLib.Exceptions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;
using GraphLib.Serialization.Interfaces;
using GraphViewModel;
using System;
using System.Configuration;
using System.Drawing;
using System.Linq;
using static ConsoleVersion.InputClass.Input;
using Console = Colorful.Console;

namespace ConsoleVersion.ViewModel
{
    internal sealed class MainViewModel : MainModel
    {
        private const int ExitCode = 0;

        public MainViewModel(BaseGraphFieldFactory fieldFactory,
            IVertexEventHolder eventHolder,
            IGraphSerializer graphSerializer,
            IGraphAssembler graphFactory,
            IPathInput pathInput) : base(fieldFactory, eventHolder, graphSerializer, graphFactory, pathInput)
        {

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
                model.OnEventHappened += OnExternalEventHappened;
                var view = new GraphCreateView(model);
                view.Start();
            }
            catch (Exception ex)
            {
                OnExceptionCaught(ex);
                Logger.Instance.Error(ex);
            }
        }

        [MenuItem("Find path", MenuItemPriority.High)]
        public override void FindPath()
        {
            try
            {
                string pluginPath = GetAlgorithmsLoadPath();
                var assembleClasses = new ConcreteAssembleAlgorithmClasses(pluginPath);
                assembleClasses.LoadClasses();
                var model = new PathFindingViewModel(assembleClasses, this, EndPoints);
                model.OnEventHappened += OnExternalEventHappened;
                var view = new PathFindView(model);
                view.Start();
            }
            catch (NoVerticesToChooseAsEndPointsException ex)
            {
                OnExceptionCaught(ex);
                Logger.Instance.Warn(ex);
            }
            catch (Exception ex)
            {
                OnExceptionCaught(ex);
                Logger.Instance.Error(ex);
            }
        }

        [MenuItem("Reverse vertex")]
        public void ReverseVertex()
        {
            if (Graph.Vertices.Any() && Graph is Graph2D graph2D)
            {
                var upperPossibleXValue = graph2D.Width - 1;
                var upperPossibleYValue = graph2D.Length - 1;

                var point = InputPoint(upperPossibleXValue, upperPossibleYValue);

                if (Graph[point] is Vertex vertex)
                {
                    vertex.Reverse();
                }
            }
        }

        [MenuItem("Change cost range", MenuItemPriority.Low)]
        public void ChangeVertexCostValueRange()
        {
            string message = "Enter upper vertex cost value: ";
            var upperValueRange = InputNumber(message, 99, 1);
            BaseVertexCost.CostRange = new ValueRange(upperValueRange, 1);
        }

        [MenuItem("Change vertex cost", MenuItemPriority.Low)]
        public void ChangeVertexCost()
        {
            if (Graph.Vertices.Any() && Graph is Graph2D graph2D)
            {
                var upperPossibleXValue = graph2D.Width - 1;
                var upperPossibleYValue = graph2D.Length - 1;

                var point = InputPoint(upperPossibleXValue, upperPossibleYValue);

                var vertex = Graph[point];
                while (vertex.IsObstacle)
                {
                    point = InputPoint(upperPossibleXValue, upperPossibleYValue);
                    vertex = Graph[point];
                }
                (vertex as Vertex)?.ChangeCost();
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

        [MenuItem("Quit program", MenuItemPriority.Lowest)]
        public void Quit()
        {
            Environment.Exit(ExitCode);
        }

        public void DisplayGraph()
        {
            try
            {
                Console.Clear();
                DisplayMainScreen();
                if (MainView.PathfindingStatisticsPosition is Coordinate2D position)
                {
                    Console.SetCursorPosition(position.X, position.Y + 1);
                }
                Console.CursorVisible = true;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.Clear();
                OnExceptionCaught(ex);
                Logger.Instance.Warn(ex);
            }
            catch (Exception ex)
            {
                Console.Clear();
                OnExceptionCaught(ex);
                Logger.Instance.Error(ex);
            }
        }

        private void DisplayMainScreen()
        {
            Console.ForegroundColor = Color.White;
            Console.WriteLine(GraphParametres);
            (GraphField as GraphField)?.ShowGraphWithFrames();
            Console.WriteLine(PathFindingStatistics);
        }

        protected override void OnExternalEventHappened(string message)
        {
            DisplayGraph();
            Console.WriteLine(message);
            Console.ReadLine();
        }

        protected override string GetAlgorithmsLoadPath()
        {
            return ConfigurationManager.AppSettings["pluginsPath"];
        }

        protected override void OnExceptionCaught(Exception ex, string additaionalMessage = "")
        {
            Console.WriteLine(ex.Message + additaionalMessage);
            Console.ReadLine();
        }
    }
}

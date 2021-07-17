using AssembleClassesLib.Interface;
using Common.ValueRanges;
using ConsoleVersion.Attributes;
using ConsoleVersion.Enums;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using GraphLib.Base;
using GraphLib.Exceptions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Graphs;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Interruptable.EventHandlers;
using Interruptable.Interface;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;

using static ConsoleVersion.InputClass.Input;
using static ConsoleVersion.Resource.Resources;
using Console = Colorful.Console;

namespace ConsoleVersion.ViewModel
{
    internal sealed class MainViewModel : MainModel, IInterruptable
    {
        private const int Yes = 1;
        private const int No = 0;

        public event InterruptEventHanlder OnInterrupted;

        public bool IsAppClosureRequested { get; private set; }

        public MainViewModel(
            IGraphFieldFactory fieldFactory,
            IVertexEventHolder eventHolder,
            ISaveLoadGraph saveLoad,
            IEnumerable<IGraphAssemble> graphAssembles,
            IAssembleClasses algorithms,
            ILog log)
            : base(fieldFactory, eventHolder, saveLoad,
                  graphAssembles, algorithms, log)
        {
            IsAppClosureRequested = false;
        }

        [MenuItem(Constants.MakeUnwieghted)]
        public void MakeGraphUnweighted()
        {
            Graph.ToUnweighted();
        }

        [MenuItem(Constants.MakeWeighted)]
        public void MakeGraphWeighted()
        {
            Graph.ToWeighted();
        }

        [MenuItem(Constants.CreateNewGraph, MenuItemPriority.Highest)]
        public override void CreateNewGraph()
        {
            try
            {
                var model = new GraphCreatingViewModel(log, this, graphAssembles);
                var view = new GraphCreateView(model);
                view.Start();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        [MenuItem(Constants.FindPath, MenuItemPriority.High)]
        public override void FindPath()
        {
            try
            {
                algorithmClasses.LoadClasses();
                var model = new PathFindingViewModel(log, algorithmClasses, this, EndPoints);
                var view = new PathFindView(model);
                view.Start();
            }
            catch (NoVerticesToChooseAsEndPointsException ex)
            {
                log.Warn(ex);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        [MenuItem(Constants.ReverseVertex)]
        public void ReverseVertex()
        {
            if (Graph.HasVertices() && Graph is Graph2D graph2D)
            {
                int upperPossibleXValue = graph2D.Width - 1;
                int upperPossibleYValue = graph2D.Length - 1;

                var point = InputPoint(upperPossibleXValue, upperPossibleYValue);

                if (Graph[point] is Vertex vertex)
                {
                    vertex.Reverse();
                }
            }
        }

        [MenuItem(Constants.ChangeCostRange, MenuItemPriority.Low)]
        public void ChangeVertexCostValueRange()
        {
            string message = "Enter upper vertex cost value: ";
            var upperValueRange = InputNumber(message, 99, 1);
            BaseVertexCost.CostRange = new InclusiveValueRange<int>(upperValueRange, 1);
        }

        [MenuItem(Constants.ChangeVertexCost, MenuItemPriority.Low)]
        public void ChangeVertexCost()
        {
            if (Graph.HasVertices() && Graph is Graph2D graph2D)
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

        [MenuItem(Constants.SaveGraph)]
        public override void SaveGraph()
        {
            base.SaveGraph();
        }

        [MenuItem(Constants.LoadGraph)]
        public override void LoadGraph()
        {
            base.LoadGraph();
            MainView.UpdatePositionOfVisualElements(Graph);
        }

        [MenuItem(Constants.QuitProgramm, MenuItemPriority.Lowest)]
        public void Interrupt()
        {
            int input = InputNumber(ExitAppMsg, Yes, No);
            IsAppClosureRequested = input == Yes;
            if (IsAppClosureRequested)
            {
                OnInterrupted?.Invoke(this, new InterruptEventArgs());
            }
        }

        public void DisplayGraph()
        {
            try
            {
                Console.Clear();
                DisplayMainScreen();
                var position = MainView.PathfindingStatisticsPosition;
                if (position != null)
                {
                    Console.SetCursorPosition(position.X, position.Y + 1);
                }
                Console.CursorVisible = true;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                log.Warn(ex);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void DisplayMainScreen()
        {
            Console.ForegroundColor = Color.White;
            Console.WriteLine(GraphParametres);
            (GraphField as GraphField)?.ShowGraphWithFrames();
            Console.WriteLine(PathFindingStatistics);
        }
    }
}
using ConsoleVersion.Attributes;
using ConsoleVersion.Enums;
using ConsoleVersion.EventArguments;
using ConsoleVersion.EventHandlers;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using ConsoleVersion.View.Interface;
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
using static GraphLib.Base.BaseVertexCost;
using Console = Colorful.Console;

namespace ConsoleVersion.ViewModel
{
    internal sealed class MainViewModel : MainModel, IInterruptable
    {
        private const int Yes = 1;
        private const int No = 0;

        public event CostRangeChangedEventHandler CostRangeChanged;
        public event NewGraphCreatedEventHandler NewGraphCreated;
        public event InterruptEventHanlder Interrupted;
        public event StatisticsUpdatedEventHandler StatisticsUpdated;

        public override string PathFindingStatistics
        {
            get => base.PathFindingStatistics;
            set
            {
                base.PathFindingStatistics = value;
                StatisticsUpdated?.Invoke(this, new StatisticsUpdatedEventArgs(value));
            }
        }

        public override IGraph Graph
        {
            get => base.Graph;
            protected set
            {
                base.Graph = value;
                NewGraphCreated?.Invoke(this, new NewGraphCreatedEventArgs(value));
            }
        }

        public MainViewModel(IGraphFieldFactory fieldFactory, IVertexEventHolder eventHolder,
            ISaveLoadGraph saveLoad, IEnumerable<IGraphAssemble> graphAssembles, ILog log)
            : base(fieldFactory, eventHolder, saveLoad, graphAssembles, log)
        {

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
                model.Interrupted += view.OnInterrupted;
                view.OnNewMenuIteration += DisplayGraph;
                view.Start();
                model.Interrupted -= view.OnInterrupted;
                view.OnNewMenuIteration -= DisplayGraph;
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
                var model = new PathFindingViewModel(log, this, EndPoints);
                var view = new PathFindView(model);
                model.Interrupted += view.OnInterrupted;
                view.OnNewMenuIteration += DisplayGraph;
                view.Start();
                model.Interrupted -= view.OnInterrupted;
                view.OnNewMenuIteration -= DisplayGraph;
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
                var vertex = InputVertex(graph2D);
                (vertex as Vertex)?.Reverse();
            }
        }

        [MenuItem(Constants.ChangeCostRange, MenuItemPriority.Low)]
        public void ChangeVertexCostValueRange()
        {
            CostRange = InputRange(Constants.VerticesCostRange);
            var args = new CostRangeChangedEventArgs(CostRange);
            CostRangeChanged?.Invoke(this, args);
        }

        [MenuItem(Constants.ChangeVertexCost, MenuItemPriority.Low)]
        public void ChangeVertexCost()
        {
            if (Graph.HasVertices() && Graph is Graph2D graph2D)
            {
                var vertex = InputVertex(graph2D);
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
            CostRangeChanged?.Invoke(this, new CostRangeChangedEventArgs(CostRange));
        }

        [MenuItem(Constants.Exit, MenuItemPriority.Lowest)]
        public void Interrupt()
        {
            int input = InputNumber(ExitAppMsg, Yes, No);
            bool isInterruptRequested = input == Yes;
            if (isInterruptRequested)
            {
                Interrupted?.Invoke(this, new InterruptEventArgs());
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
            (GraphField as IDisplayable)?.Display();
            Console.WriteLine(PathFindingStatistics);
        }
    }
}
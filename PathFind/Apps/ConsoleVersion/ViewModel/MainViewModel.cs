using ConsoleVersion.Attributes;
using ConsoleVersion.Enums;
using ConsoleVersion.EventArguments;
using ConsoleVersion.EventHandlers;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using ConsoleVersion.View.Abstraction;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;
using GraphLib.Serialization.Exceptions;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Interruptable.EventArguments;
using Interruptable.EventHandlers;
using Interruptable.Interface;
using Logging.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;

using static ConsoleVersion.Resource.Resources;
using static GraphLib.Base.BaseVertexCost;
using Console = Colorful.Console;

namespace ConsoleVersion.ViewModel
{
    internal sealed class MainViewModel : MainModel,
        IMainModel, IModel, IInterruptable, IRequireAnswerInput, IRequireInt32Input
    {
        public event CostRangeChangedEventHandler CostRangeChanged;
        public event NewGraphCreatedEventHandler NewGraphCreated;
        public event ProcessEventHandler Interrupted;
        public event StatisticsUpdatedEventHandler StatisticsUpdated;

        private string statistics;
        public string PathFindingStatistics
        {
            get => statistics;
            set { statistics = value; StatisticsUpdated?.Invoke(this, new StatisticsUpdatedEventArgs(value)); }
        }

        public override IGraph Graph
        {
            get => base.Graph;
            protected set { base.Graph = value; NewGraphCreated?.Invoke(this, new NewGraphCreatedEventArgs(value)); }
        }

        public IValueInput<int> Int32Input { get; set; }
        public IValueInput<Answer> AnswerInput { get; set; }

        public MainViewModel(IGraphFieldFactory fieldFactory, IVertexEventHolder eventHolder,
            ISaveLoadGraph saveLoad, IEnumerable<IGraphAssemble> graphAssembles, BaseEndPoints endPoints, ILog log)
            : base(fieldFactory, eventHolder, saveLoad, graphAssembles, endPoints, log)
        {

        }

        [MenuItem(Constants.MakeUnwieghted)]
        public void MakeGraphUnweighted() => Graph.ToUnweighted();

        [MenuItem(Constants.MakeWeighted)]
        public void MakeGraphWeighted() => Graph.ToWeighted();

        [MenuItem(Constants.CreateNewGraph, MenuItemPriority.Highest)]
        public override void CreateNewGraph()
        {
            try
            {
                var model = new GraphCreatingViewModel(log, this, graphAssembles);
                var view = new GraphCreateView(model);
                PrepareViewAndModel(view, model);
                view.Start();
                model.Interrupted -= view.OnInterrupted;
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
                var model = new PathFindingViewModel(log, this, endPoints);
                var view = new PathFindView(model);
                PrepareViewAndModel(view, model);
                model.AnswerInput = AnswerInput;
                view.Start();
                model.Interrupted -= view.OnInterrupted;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        [MenuItem(Constants.ReverseVertex)]
        public void ReverseVertex() => PerformActionOnVertex(vertex => vertex?.Reverse());

        [MenuItem(Constants.ChangeCostRange, MenuItemPriority.Low)]
        public void ChangeVertexCostValueRange()
        {
            CostRange = Int32Input.InputRange(Constants.VerticesCostRange);
            var args = new CostRangeChangedEventArgs(CostRange);
            CostRangeChanged?.Invoke(this, args);
        }

        [MenuItem(Constants.ChangeVertexCost, MenuItemPriority.Low)]
        public void ChangeVertexCost() => PerformActionOnVertex(vertex => vertex?.ChangeCost());

        [MenuItem(Constants.SaveGraph)]
        public override void SaveGraph() => base.SaveGraph();

        [MenuItem(Constants.LoadGraph)]
        public override void LoadGraph()
        {
            try
            {
                var graph = saveLoad.LoadGraphAsync().Result;
                ConnectNewGraph(graph);
                CostRangeChanged?.Invoke(this, new CostRangeChangedEventArgs(CostRange));
            }
            catch (CantSerializeGraphException ex)
            {
                log.Warn(ex);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        [MenuItem(Constants.Exit, MenuItemPriority.Lowest)]
        public void Interrupt()
        {
            var answer = AnswerInput.InputValue(ExitAppMsg, Constants.AnswerValueRange);
            if (answer == Answer.Yes)
            {
                Interrupted?.Invoke(this, new ProcessEventArgs());
            }
        }

        public override void ClearGraph()
        {
            base.ClearGraph();
            PathFindingStatistics = string.Empty;
        }

        public override void ConnectNewGraph(IGraph graph)
        {
            base.ConnectNewGraph(graph);
            PathFindingStatistics = string.Empty;
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

        private void PrepareViewAndModel<TModel>(View<TModel> view, TModel model)
            where TModel : IModel, IRequireInt32Input, IInterruptable
        {
            model.Interrupted += view.OnInterrupted;
            view.NewMenuIteration += DisplayGraph;
            model.Int32Input = Int32Input;
            view.Int32Input = Int32Input;
        }

        private void PerformActionOnVertex(Action<Vertex> function)
        {
            if (Graph.HasVertices() && Graph is Graph2D graph2D)
            {
                var vertex = Int32Input.InputVertex(graph2D);
                function(vertex as Vertex);
            }
        }
    }
}
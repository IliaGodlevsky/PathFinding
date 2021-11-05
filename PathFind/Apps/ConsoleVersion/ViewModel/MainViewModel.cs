using Algorithm.Factory;
using ConsoleVersion.Attributes;
using ConsoleVersion.Enums;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using ConsoleVersion.Model;
using ConsoleVersion.View;
using ConsoleVersion.View.Abstraction;
using GalaSoft.MvvmLight.Messaging;
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

using static GraphLib.Base.BaseVertexCost;
using Console = Colorful.Console;

namespace ConsoleVersion.ViewModel
{
    internal sealed class MainViewModel : MainModel,
        IMainModel, IModel, IInterruptable, IRequireAnswerInput, IRequireInt32Input
    {
        public event ProcessEventHandler Interrupted;

        public IValueInput<int> Int32Input { get; set; }
        public IValueInput<Answer> AnswerInput { get; set; }

        public MainViewModel(IGraphFieldFactory fieldFactory, IVertexEventHolder eventHolder, ISaveLoadGraph saveLoad,
            IEnumerable<IGraphAssemble> graphAssembles, BaseEndPoints endPoints, IEnumerable<IAlgorithmFactory> algorithmFactories, ILog log)
            : base(fieldFactory, eventHolder, saveLoad, graphAssembles, endPoints, algorithmFactories, log)
        {
            Messenger.Default.Register<GraphCreatedMessage>(this, MessageTokens.MainModel, message => ConnectNewGraph(message.Graph));
            Messenger.Default.Register<ClearGraphMessage>(this, MessageTokens.MainModel, message => ClearGraph());
            Messenger.Default.Register<ClearColorsMessage>(this, MessageTokens.MainModel, message => ClearColors());
        }

        [MenuItem(MenuItemsNames.MakeUnwieghted, MenuItemPriority.Normal)]
        public void MakeGraphUnweighted() => Graph.ToUnweighted();

        [MenuItem(MenuItemsNames.MakeWeighted, MenuItemPriority.Normal)]
        public void MakeGraphWeighted() => Graph.ToWeighted();

        [MenuItem(MenuItemsNames.CreateNewGraph, MenuItemPriority.Highest)]
        public override void CreateNewGraph()
        {
            try
            {
                var model = new GraphCreatingViewModel(log, graphAssembles);
                var view = new GraphCreateView(model);
                PrepareViewAndModel(view, model);
                view.Start();
                view.NewMenuIteration -= DisplayGraph;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        [MenuItem(MenuItemsNames.FindPath, MenuItemPriority.High)]
        public override void FindPath()
        {
            try
            {
                var model = new PathFindingViewModel(log, Graph, endPoints, algorithmFactories);
                var view = new PathFindView(model);
                PrepareViewAndModel(view, model);
                model.AnswerInput = AnswerInput;
                view.Start();
                view.NewMenuIteration -= DisplayGraph;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        [MenuItem(MenuItemsNames.ReverseVertex, MenuItemPriority.Normal)]
        public void ReverseVertex() => PerformActionOnVertex(vertex => vertex?.Reverse());

        [MenuItem(MenuItemsNames.ChangeCostRange, MenuItemPriority.Low)]
        public void ChangeVertexCostValueRange()
        {
            CostRange = Int32Input.InputRange(Constants.VerticesCostRange);
            var message = new CostRangeChangedMessage(CostRange);
            Messenger.Default.Send(message, MessageTokens.MainView);
        }

        [MenuItem(MenuItemsNames.ChangeVertexCost, MenuItemPriority.Low)]
        public void ChangeVertexCost() => PerformActionOnVertex(vertex => vertex?.ChangeCost());

        [MenuItem(MenuItemsNames.SaveGraph, MenuItemPriority.Normal)]
        public override void SaveGraph() => base.SaveGraph();

        [MenuItem(MenuItemsNames.LoadGraph, MenuItemPriority.Normal)]
        public override void LoadGraph()
        {
            try
            {
                var graph = saveLoad.LoadGraphAsync().Result;
                ConnectNewGraph(graph);
                var costRangeMessage = new CostRangeChangedMessage(CostRange);
                Messenger.Default.Send(costRangeMessage, MessageTokens.MainView);
                var graphMessage = new GraphCreatedMessage(graph);
                Messenger.Default.Send(graphMessage, MessageTokens.MainView);
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

        [MenuItem(MenuItemsNames.Exit, MenuItemPriority.Lowest)]
        public void Interrupt()
        {
            var answer = AnswerInput.InputValue(MessagesTexts.ExitAppMsg, Constants.AnswerValueRange);
            if (answer == Answer.Yes)
            {
                Interrupted?.Invoke(this, new ProcessEventArgs());
            }
        }

        public override void ClearGraph()
        {
            base.ClearGraph();
            var message = new UpdateStatisticsMessage(string.Empty);
            Messenger.Default.Send(message, MessageTokens.MainView);
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
            Console.WriteLine();
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
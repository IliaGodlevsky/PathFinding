using Autofac;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Enums;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using ConsoleVersion.Model;
using ConsoleVersion.ValueInput;
using ConsoleVersion.Views;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;
using GraphLib.Serialization;
using GraphLib.Serialization.Exceptions;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using static GraphLib.Base.BaseVertexCost;
using Console = Colorful.Console;

namespace ConsoleVersion.ViewModel
{
    internal sealed class MainViewModel : MainModel,
        IMainModel, IViewModel, IRequireAnswerInput, IRequireInt32Input, IDisposable
    {
        public event Action WindowClosed;

        public ConsoleValueInput<int> Int32Input { get; set; }
        public ConsoleValueInput<Answer> AnswerInput { get; set; }

        public MainViewModel(IGraphFieldFactory fieldFactory,
            IVertexEventHolder eventHolder, GraphSerializationModule serializationModule, BaseEndPoints endPoints, ILog log)
            : base(fieldFactory, eventHolder, serializationModule, endPoints, log)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, MessageTokens.MainModel, message => ConnectNewGraph(message.Graph));
            messenger.Register<ClearGraphMessage>(this, MessageTokens.MainModel, message => ClearGraph());
            messenger.Register<ClearColorsMessage>(this, MessageTokens.MainModel, message => ClearColors());
            messenger.Register<ClaimGraphMessage>(this, MessageTokens.MainModel, RecieveClaimGraphMessage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [MenuItem(MenuItemsNames.MakeUnwieghted, MenuItemPriority.Normal)]
        public void MakeGraphUnweighted() => Graph.ToUnweighted();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [MenuItem(MenuItemsNames.MakeWeighted, MenuItemPriority.Normal)]
        public void MakeGraphWeighted() => Graph.ToWeighted();

        [MenuItem(MenuItemsNames.CreateNewGraph, MenuItemPriority.Highest)]
        public override void CreateNewGraph()
        {
            try
            {
                using (var scope = DI.Container.BeginLifetimeScope())
                {
                    var view = scope.Resolve<GraphCreateView>();
                    view.Start();
                }
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
                using (var scope = DI.Container.BeginLifetimeScope())
                {
                    var view = scope.Resolve<PathFindView>();
                    view.Start();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [MenuItem(MenuItemsNames.ReverseVertex, MenuItemPriority.Normal)]
        public void ReverseVertex() => PerformActionOnVertex(vertex => vertex?.OnVertexReversed());

        [MenuItem(MenuItemsNames.ChangeCostRange, MenuItemPriority.Low)]
        public void ChangeVertexCostValueRange()
        {
            CostRange = Int32Input.InputRange(Constants.VerticesCostRange);
            var message = new CostRangeChangedMessage(CostRange);
            messenger.Forward(message, MessageTokens.MainView);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [MenuItem(MenuItemsNames.ChangeVertexCost, MenuItemPriority.Low)]
        public void ChangeVertexCost() => PerformActionOnVertex(vertex => vertex?.OnVertexCostChanged());

        [MenuItem(MenuItemsNames.SaveGraph, MenuItemPriority.Normal)]
        public override void SaveGraph() => base.SaveGraph();

        [MenuItem(MenuItemsNames.LoadGraph, MenuItemPriority.Normal)]
        public override void LoadGraph()
        {
            try
            {
                var graph = serializationModule.LoadGraph();
                ConnectNewGraph(graph);
                messenger
                    .Forward(new CostRangeChangedMessage(CostRange), MessageTokens.MainView)
                    .Forward(new GraphCreatedMessage(graph), MessageTokens.MainView);
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
                WindowClosed?.Invoke();
            }
        }

        public override void ClearGraph()
        {
            base.ClearGraph();
            messenger.Forward(new UpdateStatisticsMessage(string.Empty), MessageTokens.MainView);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RecieveClaimGraphMessage(ClaimGraphMessage message)
        {
            messenger.Forward(new GraphCreatedMessage(Graph), message.ClaimerMessageToken);
        }

        private void PerformActionOnVertex(Action<Vertex> function)
        {
            if (Graph.HasVertices() && Graph is Graph2D graph2D)
            {
                var vertex = Int32Input.InputVertex(graph2D);
                function(vertex as Vertex);
            }
        }

        public void Dispose()
        {
            messenger.Unregister(this);
            WindowClosed = null;
        }

        private readonly IMessenger messenger;
    }
}
using Autofac;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.DependencyInjection;
using ConsoleVersion.Enums;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using ConsoleVersion.Model;
using ConsoleVersion.Views;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base.EndPoints;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using GraphLib.Serialization;
using GraphViewModel;
using GraphViewModel.Interfaces;
using Logging.Interface;
using NullObject.Extensions;
using System;
using System.Drawing;
using static GraphLib.Base.BaseVertexCost;
using Console = Colorful.Console;

namespace ConsoleVersion.ViewModel
{
    internal sealed class MainViewModel : MainModel, IMainModel, IViewModel, IRequireAnswerInput, IRequireIntInput, IDisposable
    {
        public event Action WindowClosed;

        private readonly IMessenger messenger;

        public IInput<int> IntInput { get; set; }

        public IInput<Answer> AnswerInput { get; set; }

        public MainViewModel(IGraphFieldFactory fieldFactory, IVertexEventHolder eventHolder,
            GraphSerializationModule serializationModule, BaseEndPoints endPoints, ILog log)
            : base(fieldFactory, eventHolder, serializationModule, endPoints, log)
        {
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, MessageTokens.MainModel, message => ConnectNewGraph(message.Graph));
            messenger.Register<ClearGraphMessage>(this, MessageTokens.MainModel, message => ClearGraph());
            messenger.Register<ClearColorsMessage>(this, MessageTokens.MainModel, message => ClearColors());
            messenger.Register<ClaimGraphMessage>(this, MessageTokens.MainModel, RecieveClaimGraphMessage);
        }

        [PreValidationMethod(nameof(IsGraphEmpty))]
        [MenuItem(MenuItemsNames.MakeUnwieghted, 2)]
        public void MakeGraphUnweighted()
        {
            Graph.ToUnweighted();
        }

        [PreValidationMethod(nameof(IsGraphEmpty))]
        [MenuItem(MenuItemsNames.MakeWeighted, 3)]
        public void MakeGraphWeighted()
        {
            Graph.ToWeighted();
        }

        [MenuItem(MenuItemsNames.CreateNewGraph, 0)]
        public override void CreateNewGraph()
        {
            DI.Container.Display<GraphCreateView>();
        }

        [PreValidationMethod(nameof(IsGraphEmpty))]
        [MenuItem(MenuItemsNames.FindPath, 1)]
        public override void FindPath()
        {
            DI.Container.Display<PathFindView>();
        }

        [PreValidationMethod(nameof(IsGraphEmpty))]
        [MenuItem(MenuItemsNames.ReverseVertex, 4)]
        public void ReverseVertex()
        {
            ChangeVertexState(vertex => vertex.OnVertexReversed());
        }

        [PreValidationMethod(nameof(IsGraphEmpty))]
        [MenuItem(MenuItemsNames.ChangeVertexCost, 7)]
        public void ChangeVertexCost()
        {
            ChangeVertexState(vertex => vertex.OnVertexCostChanged());
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.ChangeCostRange, 8)]
        public void ChangeVertexCostValueRange()
        {
            CostRange = IntInput.InputRange(Constants.VerticesCostRange);
            var message = new CostRangeChangedMessage(CostRange);
            messenger.Forward(message, MessageTokens.MainView);
        }

        [PreValidationMethod(nameof(IsGraphEmpty))]
        [MenuItem(MenuItemsNames.SaveGraph, 5)]
        public override void SaveGraph()
        {
            base.SaveGraph();
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.LoadGraph, 6)]
        public override void LoadGraph()
        {
            var graph = serializationModule.LoadGraph();
            ConnectNewGraph(graph);
            messenger
                .Forward(new CostRangeChangedMessage(CostRange), MessageTokens.MainView)
                .Forward(new GraphCreatedMessage(graph), MessageTokens.MainView);
        }

        [PreValidationMethod(nameof(CanExecuteInterrupt))]
        [MenuItem(MenuItemsNames.Exit, 9)]
        public void Interrupt()
        {
            WindowClosed?.Invoke();
        }

        public override void ClearGraph()
        {
            base.ClearGraph();
            messenger.Forward(UpdateStatisticsMessage.Empty, MessageTokens.MainView);
        }

        public void DisplayGraph()
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = Color.White;
                Console.WriteLine(GraphParametres);
                (GraphField as IDisplayable)?.Display();
                Console.WriteLine();
                MainView.SetCursorPositionUnderMenu(1);
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

        private void RecieveClaimGraphMessage(ClaimGraphMessage message)
        {
            if (!Graph.IsNull())
            {
                messenger.Forward(new GraphCreatedMessage(Graph), message.ClaimerMessageToken);
            }
        }

        private void ChangeVertexState(Action<Vertex> changeFunction)
        {
            changeFunction(IntInput.InputVertex((Graph2D)Graph));
        }

        private bool IsGraphEmpty()
        {
            return Graph.HasVertices();
        }

        private bool CanExecuteInterrupt()
        {
            return AnswerInput.Input(MessagesTexts.ExitAppMsg, Constants.AnswerValueRange) == Answer.Yes;
        }

        private void ExecuteSafe(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public void Dispose()
        {
            messenger.Unregister(this);
            WindowClosed = null;
        }
    }
}
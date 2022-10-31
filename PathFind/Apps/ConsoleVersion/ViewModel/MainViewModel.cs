using Autofac;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.ConvertedProperties;
using ConsoleVersion.Delegates;
using ConsoleVersion.DependencyInjection;
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
using Logging.Interface;
using System;
using System.Drawing;
using static GraphLib.Base.BaseVertexCost;
using Console = Colorful.Console;

namespace ConsoleVersion.ViewModel
{
    internal sealed class MainViewModel : IViewModel, ICache<Graph2D<Vertex>>, IRequireAnswerInput, IRequireIntInput, IDisposable
    {
        public event Action WindowClosed;

        private readonly ILog log;
        private readonly IMessenger messenger;
        private readonly IGraphEvents<Vertex> events;
        private readonly IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField> fieldFactory;
        private readonly BaseEndPoints<Vertex> endPoints;

        private GraphField graphField;

        public IProperty<string> GraphParamters { get; private set; }

        public IInput<int> IntInput { get; set; }

        public IInput<Answer> AnswerInput { get; set; }

        public Graph2D<Vertex> Cached { get; private set; }

        public MainViewModel(IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField> fieldFactory,
            IGraphEvents<Vertex> events, BaseEndPoints<Vertex> endPoints, ILog log)
        {
            Cached = Graph2D<Vertex>.Empty;
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, SetGraph);
            messenger.Register<ClearGraphMessage>(this, message => ClearGraph());
            messenger.Register<ClearColorsMessage>(this, message => ClearColors());
            this.fieldFactory = fieldFactory;
            this.events = events;
            this.endPoints = endPoints;
            this.log = log;
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.CreateNewGraph, 0)]
        private void CreateNewGraph() => DI.Container.Display<GraphCreateView>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.FindPath, 1)]
        private void FindPath() => DI.Container.Display<PathFindView>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.SmoothGraph, 2)]
        private void SmoothGraph() => DI.Container.Display<GraphSmoothView>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.ChangedVertexState, 3)]
        private void ChangeVertexState() => DI.Container.Display<VertexStateView>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.SaveGraph, 4)]
        private void SaveGraph() => DI.Container.Display<GraphSaveView>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.LoadGraph, 5)]
        private void LoadGraph() => DI.Container.Display<GraphLoadView>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.ChangeCostRange, 6)]
        private void ChangeVertexCostValueRange()
        {
            CostRange = IntInput.InputRange(Constants.VerticesCostRange);
            messenger.Send(new CostRangeChangedMessage(CostRange));
        }

        [Condition(nameof(CanExecuteInterrupt))]
        [MenuItem(MenuItemsNames.Exit, 7)]
        private void Interrupt() => WindowClosed?.Invoke();

        public void ClearGraph()
        {
            Cached.Refresh();
            GraphParamters = GraphParamsProperty.Empty;
            endPoints.Reset();
            messenger.Send(UpdateStatisticsMessage.Empty);
        }

        public void ClearColors()
        {
            Cached.Refresh();
            endPoints.RestoreCurrentColors();
        }

        public void DisplayGraph()
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = Color.White;
                Console.WriteLine(GraphParamters);
                graphField?.Display();
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

        private void SetGraph(GraphCreatedMessage message)
        {
            endPoints.Reset();
            events.Unsubscribe(Cached);
            Cached = message.Graph;
            graphField = fieldFactory.CreateGraphField(Cached);
            events.Subscribe(Cached);
            GraphParamters = GraphParamsProperty.Assign(Cached);
        }

        private bool IsGraphValid()
        {
            return Cached != Graph2D<Vertex>.Empty && Cached.Count > 0;
        }

        private bool CanExecuteInterrupt()
        {
            return AnswerInput.Input(MessagesTexts.ExitAppMsg, Answer.Range);
        }

        private void ExecuteSafe(Command action)
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
using Autofac;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.ConvertedProperties;
using Pathfinding.App.Console.Delegates;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Views;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Core.Abstractions;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using System;
using System.Drawing;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class MainViewModel : IViewModel, ICache<Graph2D<Vertex>>, IRequireAnswerInput, IRequireIntInput, IDisposable
    {
        public event Action ViewClosed;

        private readonly ILog log;
        private readonly IMessenger messenger;
        private readonly IGraphSubscription<Vertex> subscription;
        private readonly IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField> fieldFactory;
        private readonly VisualPathfindingRange<Vertex> pathfindingRange;

        private GraphField graphField;

        public IProperty<string> GraphParamters { get; private set; }

        public IInput<int> IntInput { get; set; }

        public IInput<Answer> AnswerInput { get; set; }

        public Graph2D<Vertex> Cached { get; private set; }

        public MainViewModel(IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField> fieldFactory,
            IGraphSubscription<Vertex> events, VisualPathfindingRange<Vertex> endPoints, ILog log)
        {
            Cached = Graph2D<Vertex>.Empty;
            messenger = DI.Container.Resolve<IMessenger>();
            messenger.Register<GraphCreatedMessage>(this, SetGraph);
            messenger.Register<ClearGraphMessage>(this, message => ClearGraph());
            messenger.Register<ClearColorsMessage>(this, message => ClearColors());
            this.fieldFactory = fieldFactory;
            subscription = events;
            this.pathfindingRange = endPoints;
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
            VertexCost.CostRange = IntInput.InputRange(Constants.VerticesCostRange);
            messenger.Send(new CostRangeChangedMessage(VertexCost.CostRange));
        }

        [Condition(nameof(CanExecuteInterrupt))]
        [MenuItem(MenuItemsNames.Exit, 7)]
        private void Interrupt() => ViewClosed?.Invoke();

        public void ClearGraph()
        {
            Cached.RestoreVerticesVisualState();
            GraphParamters = GraphParamsProperty.Empty;
            pathfindingRange.Undo();
            messenger.Send(UpdateStatisticsMessage.Empty);
        }

        public void ClearColors()
        {
            Cached.RestoreVerticesVisualState();
            pathfindingRange.RestoreVerticesVisualState();
        }

        public void DisplayGraph()
        {
            try
            {
                ColorfulConsole.Clear();
                ColorfulConsole.ForegroundColor = Color.White;
                ColorfulConsole.WriteLine(GraphParamters);
                graphField?.Display();
                ColorfulConsole.WriteLine();
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
            pathfindingRange.Undo();
            subscription.Unsubscribe(Cached);
            Cached = message.Graph;
            graphField = fieldFactory.CreateGraphField(Cached);
            subscription.Subscribe(Cached);
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
            ViewClosed = null;
        }
    }
}
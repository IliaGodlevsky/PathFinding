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
using Shared.Primitives.Attributes;
using System;
using System.Drawing;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class MainViewModel : ViewModel, ICache<Graph2D<Vertex>>, IRequireAnswerInput, IRequireIntInput
    {
        private readonly IMessenger messenger;
        private readonly IGraphSubscription<Vertex> subscription;
        private readonly IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField> fieldFactory;
        private readonly VisualPathfindingRange<Vertex> pathfindingRange;

        private GraphField GraphField { get; set; } = GraphField.Empty;

        private IProperty<string> GraphParamters { get; set; }

        public IInput<int> IntInput { get; set; }

        public IInput<Answer> AnswerInput { get; set; }

        public Graph2D<Vertex> Cached { get; private set; }

        public MainViewModel(IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField> fieldFactory, 
            IGraphSubscription<Vertex> subscription, 
            VisualPathfindingRange<Vertex> endPoints, IMessenger messenger, ILog log)
            : base(log)
        {
            Cached = Graph2D<Vertex>.Empty;
            this.messenger = messenger;
            messenger.Register<GraphCreatedMessage>(this, SetGraph);
            messenger.Register<ClearGraphMessage>(this, ClearGraph);
            messenger.Register<ClearColorsMessage>(this, ClearColors);
            this.fieldFactory = fieldFactory;
            this.subscription = subscription;
            this.pathfindingRange = endPoints;
        }

        [Order(0)]
        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.CreateNewGraph)]
        private void CreateNewGraph() => DI.Container.Display<GraphCreateView>();

        [Order(1)]
        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.FindPath)]
        private void FindPath() => DI.Container.Display<PathFindView>();

        [Order(2)]
        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.SmoothGraph)]
        private void SmoothGraph() => DI.Container.Display<GraphSmoothView>();

        [Order(3)]
        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.ChangedVertexState)]
        private void ChangeVertexState() => DI.Container.Display<VertexStateView>();

        [Order(4)]
        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.SaveGraph)]
        private void SaveGraph() => DI.Container.Display<GraphSaveView>();

        [Order(5)]
        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.LoadGraph)]
        private void LoadGraph() => DI.Container.Display<GraphLoadView>();

        [Order(6)]
        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.ChangeCostRange)]
        private void ChangeVertexCostValueRange()
        {
            VertexCost.CostRange = IntInput.InputRange(Constants.VerticesCostRange);
            messenger.Send(new CostRangeChangedMessage(VertexCost.CostRange));
        }

        [Order(7)]
        [Condition(nameof(CanExecuteInterrupt))]
        [MenuItem(MenuItemsNames.Exit)]
        private void Interrupt() => RaiseViewClosed();

        private void ClearGraph(ClearGraphMessage message)
        {
            Cached.RestoreVerticesVisualState();
            GraphParamters = GraphParamsProperty.Empty;
            pathfindingRange.Undo();
            messenger.Send(UpdateStatisticsMessage.Empty);
        }

        private void ClearColors(ClearColorsMessage message)
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
                GraphField.Display();
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
            GraphField = fieldFactory.CreateGraphField(Cached);
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

        public override void Dispose()
        {
            base.Dispose();
            messenger.Unregister(this);
        }
    }
}
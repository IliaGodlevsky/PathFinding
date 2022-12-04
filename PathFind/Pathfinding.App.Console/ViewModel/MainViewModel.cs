using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.ConvertedProperties;
using Pathfinding.App.Console.DependencyInjection;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    using FieldFactory = IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField>;

    [MenuColumnsNumber(3)]
    internal sealed class MainViewModel : SafeViewModel, ICache<Graph2D<Vertex>>, IRequireAnswerInput, IRequireIntInput
    {
        private readonly IMessenger messenger;
        private readonly FieldFactory fieldFactory;
        private readonly IUndo undo;

        private GraphField GraphField { get; set; } = GraphField.Empty;

        private IProperty<string> GraphParamters { get; set; }

        public IInput<int> IntInput { get; set; }

        public IInput<Answer> AnswerInput { get; set; }

        public Graph2D<Vertex> Cached { get; private set; } = Graph2D<Vertex>.Empty;

        public MainViewModel(FieldFactory fieldFactory, IMessenger messenger, IUndo undo, ILog log)
            : base(log)
        {
            Cached = Graph2D<Vertex>.Empty;
            this.undo = undo;
            this.messenger = messenger;
            this.messenger.Register<GraphCreatedMessage>(this, MessageTokens.MainViewModel, SetGraph);
            this.messenger.Register<ClearGraphMessage>(this, ClearGraph);
            this.fieldFactory = fieldFactory;
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.FindPath, 1)]
        private void FindPath() => DI.Container.Display<PathfindingViewModel>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.CreateNewGraph, 0)]
        private void CreateNewGraph() => DI.Container.Display<GraphCreatingViewModel>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.SmoothGraph, 2)]
        private void SmoothGraph() => DI.Container.Display<GraphSmoothViewModel>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.ChangedVertexState, 3)]
        private void ChangeVertexState() => DI.Container.Display<VertexStateViewModel>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.SaveGraph, 4)]
        private void SaveGraph() => DI.Container.Display<GraphSaveViewModel>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.LoadGraph, 5)]
        private void LoadGraph() => DI.Container.Display<GraphLoadViewModel>();

        protected override void RaiseViewClosed()
        {
            using (Cursor.CleanUpAfter())
            {
                if (AnswerInput.Input(MessagesTexts.ExitAppMsg, Answer.Range))
                {
                    base.RaiseViewClosed();
                }
            }
        }

        private void ClearGraph(ClearGraphMessage message)
        {
            Cached.RestoreVerticesVisualState();
            undo.Undo();
            messenger.Send(PathfindingStatisticsMessage.Empty);
        }

        private void DisplayGraph()
        {
            try
            {
                System.Console.Clear();
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine(GraphParamters);
                GraphField.Display();
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
            undo.Undo();
            Cached = message.Graph;
            GraphField = fieldFactory.CreateGraphField(Cached);
            GraphParamters = GraphParamsProperty.Assign(Cached);
            DisplayGraph();
        }

        [FailMessage(MessagesTexts.GraphIsNotCreatedMsg)]
        private bool IsGraphValid() => Cached != Graph2D<Vertex>.Empty;

        public override void Dispose()
        {
            base.Dispose();
            messenger.Unregister(this);
        }
    }
}
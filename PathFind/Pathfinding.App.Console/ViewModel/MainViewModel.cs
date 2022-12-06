using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    using FieldFactory = IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField>;

    [MenuColumnsNumber(3)]
    internal sealed class MainViewModel : SafeParentViewModel, IRequireAnswerInput, IRequireIntInput
    {
        private readonly IMessenger messenger;
        private readonly FieldFactory fieldFactory;
        private readonly IUndo undo;

        private GraphField GraphField { get; set; } = GraphField.Empty;

        public IInput<int> IntInput { get; set; }

        public IInput<Answer> AnswerInput { get; set; }

        private Graph2D<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public MainViewModel(FieldFactory fieldFactory, IMessenger messenger, IUndo undo, ILog log)
            : base(log)
        {
            this.undo = undo;
            this.messenger = messenger;
            this.fieldFactory = fieldFactory;
            this.messenger.Register<GraphCreatedMessage>(this, MessageTokens.MainViewModel, SetGraph);
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.FindPath, 1)]
        private void FindPath() => Display<PathfindingViewModel>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.CreateNewGraph, 0)]
        private void CreateNewGraph() => Display<GraphCreatingViewModel>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid), 1)]
        [Condition(nameof(IsGraphSmoothSupported))]
        [MenuItem(MenuItemsNames.SmoothGraph, 2)]
        private void SmoothGraph() => Display<GraphSmoothViewModel>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid), 1)]
        [Condition(nameof(IsVertexStateSupported))]
        [MenuItem(MenuItemsNames.ChangedVertexState, 3)]
        private void ChangeVertexState() => Display<VertexStateViewModel>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid), 1)]
        [Condition(nameof(IsGraphSaveSupported))]
        [MenuItem(MenuItemsNames.SaveGraph, 4)]
        private void SaveGraph() => Display<GraphSaveViewModel>();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphLoadSupported))]
        [MenuItem(MenuItemsNames.LoadGraph, 5)]
        private void LoadGraph() => Display<GraphLoadViewModel>();

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

        private void DisplayGraph()
        {
            try
            {
                System.Console.Clear();
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine(Graph.ToString());
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
            Graph = message.Graph;
            GraphField = fieldFactory.CreateGraphField(Graph);
            DisplayGraph();
        }

        [FailMessage(MessagesTexts.OperationIsNotSupported)]
        private bool IsGraphLoadSupported() => IsOperationSuppoted<GraphLoadViewModel>();

        [FailMessage(MessagesTexts.OperationIsNotSupported)]
        private bool IsGraphSaveSupported() => IsOperationSuppoted<GraphSaveViewModel>();

        [FailMessage(MessagesTexts.OperationIsNotSupported)]
        private bool IsVertexStateSupported() => IsOperationSuppoted<VertexStateViewModel>();

        [FailMessage(MessagesTexts.OperationIsNotSupported)]
        private bool IsGraphSmoothSupported() => IsOperationSuppoted<GraphSmoothViewModel>();

        [FailMessage(MessagesTexts.GraphIsNotCreatedMsg)]
        private bool IsGraphValid() => Graph != Graph2D<Vertex>.Empty;

        public override void Dispose()
        {
            base.Dispose();
            messenger.Unregister(this);
        }
    }
}
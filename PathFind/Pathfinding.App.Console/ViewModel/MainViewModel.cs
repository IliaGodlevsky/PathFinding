using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.App.Console.Views;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Extensions;
using Pathfinding.VisualizationLib.Core.Interface;
using Shared.Executable;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.ViewModel
{
    using FieldFactory = IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField>;

    [MenuColumnsNumber(3)]
    internal sealed class MainViewModel : SafeViewModel, IParentViewModel, IRequireAnswerInput, IRequireIntInput
    {
        private readonly IMessenger messenger;
        private readonly FieldFactory fieldFactory;
        private readonly IUndo undo;

        private GraphField GraphField { get; set; } = GraphField.Empty;

        public IInput<int> IntInput { get; set; }

        public IInput<Answer> AnswerInput { get; set; }

        public IReadOnlyCollection<IViewModel> Children { get; set; }

        private Graph2D<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public MainViewModel(FieldFactory fieldFactory, IMessenger messenger, IUndo undo, ILog log)
            : base(log)
        {
            this.undo = undo;
            this.messenger = messenger;
            this.fieldFactory = fieldFactory;
            this.messenger.Register<GraphCreatedMessage>(this, MessageTokens.MainViewModel, SetGraph);
            this.messenger.Register<ClearGraphMessage>(this, ClearGraph);           
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.FindPath, 1)]
        private void FindPath() => new View(Children.Get<PathfindingViewModel>(), log).Display();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.CreateNewGraph, 0)]
        private void CreateNewGraph() => new View(Children.Get<GraphCreatingViewModel>(), log).Display();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.SmoothGraph, 2)]
        private void SmoothGraph() => new View(Children.Get<GraphSmoothViewModel>(), log).Display();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.ChangedVertexState, 3)]
        private void ChangeVertexState() => new View(Children.Get<VertexStateViewModel>(), log).Display();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsGraphValid))]
        [MenuItem(MenuItemsNames.SaveGraph, 4)]
        private void SaveGraph() => new View(Children.Get<GraphSaveViewModel>(), log).Display();

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.LoadGraph, 5)]
        private void LoadGraph() => new View(Children.Get<GraphLoadViewModel>(), log).Display();

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
            Graph.RestoreVerticesVisualState();
            undo.Undo();
            messenger.Send(PathfindingStatisticsMessage.Empty);
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

        [FailMessage(MessagesTexts.GraphIsNotCreatedMsg)]
        private bool IsGraphValid() => Graph != Graph2D<Vertex>.Empty;

        public override void Dispose()
        {
            base.Dispose();
            messenger.Unregister(this);
        }
    }
}
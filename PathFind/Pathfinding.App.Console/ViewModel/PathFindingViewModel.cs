using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using Pathfinding.Visualization.Extensions;
using Shared.Executable;
using System;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(2)]
    internal sealed class PathfindingViewModel : SafeParentViewModel, IDisposable
    {       
        private readonly IMessenger messenger;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        private readonly IUndo undo;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public PathfindingViewModel(IPathfindingRangeBuilder<Vertex> rangeBuilder, 
            ILog log, IUndo undo, IMessenger messenger)
            : base(log)
        {
            this.undo = undo;
            this.rangeBuilder = rangeBuilder;
            this.messenger = messenger;
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }

        [Condition(nameof(CanEnterPathfinding))]
        [MenuItem(MenuItemsNames.FindPath, 0)]
        private void FindPath()
        {
            Display<PathfindingProcessViewModel>();
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.ChoosePathfindingRange, 1)]
        private void ChooseExtremeVertex()
        {
            Display<PathfindingRangeViewModel>();
        }

        [MenuItem(MenuItemsNames.ClearGraph, 2)]
        private void ClearGraph()
        {
            using (Cursor.HideCursor())
            {
                graph.RestoreVerticesVisualState();
                undo.Undo();
                messenger.Send(PathfindingStatisticsMessage.Empty);
            }
        }

        [FailMessage(MessagesTexts.NoPathfindingRangeMsg)]
        private bool CanEnterPathfinding()
        {
            return rangeBuilder.Range.HasSourceAndTargetSet();
        }
    }
}
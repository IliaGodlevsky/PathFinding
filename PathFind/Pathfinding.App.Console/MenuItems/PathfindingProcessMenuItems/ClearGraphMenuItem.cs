using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Visualization.Extensions;
using Shared.Executable;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    internal sealed class ClearGraphMenuItem : IMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IUndo undo;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public int Order => 6;

        public ClearGraphMenuItem(IMessenger messenger, IUndo undo)
        {
            this.messenger = messenger;
            this.undo = undo;
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        public bool CanBeExecuted() => graph != Graph2D<Vertex>.Empty;

        public void Execute()
        {
            graph.RestoreVerticesVisualState();
            undo.Undo();
            messenger.Send(PathfindingStatisticsMessage.Empty);
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }

        public override string ToString()
        {
            return "Refresh graph";
        }
    }
}

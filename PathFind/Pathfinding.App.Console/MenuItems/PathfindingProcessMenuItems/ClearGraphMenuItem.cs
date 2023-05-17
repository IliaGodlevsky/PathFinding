using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Visualization.Extensions;
using Shared.Executable;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [LowPriority]
    internal sealed class ClearGraphMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly IUndo undo;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public ClearGraphMenuItem(IMessenger messenger, IUndo undo)
        {
            this.messenger = messenger;
            this.undo = undo;
        }

        public bool CanBeExecuted() => graph != Graph2D<Vertex>.Empty;

        public void Execute()
        {
            graph.RestoreVerticesVisualState();
            undo.Undo();
            messenger.SendData(string.Empty, Tokens.AppLayout);
        }

        private void SetGraph(Graph2D<Vertex> graph)
        {
            this.graph = graph;
        }

        public override string ToString()
        {
            return Languages.ClearGraph;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }
    }
}

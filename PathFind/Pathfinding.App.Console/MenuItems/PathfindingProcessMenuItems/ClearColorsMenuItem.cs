using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.Visualization.Extensions;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [LowPriority]
    internal sealed class ClearColorsMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        private IGraph<Vertex> graph = Graph<Vertex>.Empty;

        public ClearColorsMenuItem(IMessenger messenger,
            IPathfindingRangeBuilder<Vertex> rangeBuilder)
        {
            this.messenger = messenger;
            this.rangeBuilder = rangeBuilder;
        }

        public bool CanBeExecuted() => graph != Graph<Vertex>.Empty;

        public void Execute()
        {
            messenger.SendData(string.Empty, Tokens.AppLayout);
            graph.RestoreVerticesVisualState();
            rangeBuilder.Range.RestoreVerticesVisualState();
        }

        private void SetGraph(IGraph<Vertex> graph)
        {
            this.graph = graph;
        }

        public override string ToString()
        {
            return Languages.ClearColors;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }
    }
}

using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Random;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class ResizeGraphMenuItem : GraphCreatingMenuItem
    {
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public ResizeGraphMenuItem(IMessenger messenger, IRandom random,
            IVertexCostFactory costFactory, INeighborhoodFactory neighborhoodFactory)
            : base(messenger, random, costFactory, neighborhoodFactory)
        {
            
        }

        private void OnGraphCreated(GraphCreatedMessage msg)
        {
            graph = msg.Graph;
        }

        public override bool CanBeExecuted()
        {
            return base.CanBeExecuted()
                && (graph.Width != width || graph.Length != length)
                && graph != Graph2D<Vertex>.Empty;
        }

        protected override ILayer<Graph2D<Vertex>, Vertex>[] GetLayers()
        {
            return base.GetLayers()
                .Append(new GraphLayer(graph.Clone()))
                .ToArray();
        }

        public override void RegisterHanlders(IMessenger messenger)
        {
            base.RegisterHanlders(messenger);
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        public override string ToString()
        {
            return Languages.ResizeGraph;
        }
    }
}

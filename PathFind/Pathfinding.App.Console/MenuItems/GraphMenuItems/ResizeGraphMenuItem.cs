using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages.DataMessages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Random;
using System.Collections.Generic;
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

        private void OnGraphCreated(DataMessage<Graph2D<Vertex>> msg)
        {
            graph = msg.Value;
        }

        public override bool CanBeExecuted()
        {
            return base.CanBeExecuted()
                && (graph.Width != width || graph.Length != length)
                && graph != Graph2D<Vertex>.Empty;
        }

        protected override IEnumerable<ILayer<Graph2D<Vertex>, Vertex>> GetLayers()
        {
            return base.GetLayers().Append(new GraphLayer(graph));
        }

        public override void RegisterHanlders(IMessenger messenger)
        {
            base.RegisterHanlders(messenger);
            messenger.RegisterGraph(this, Tokens.Common, OnGraphCreated);
        }

        public override string ToString()
        {
            return Languages.ResizeGraph;
        }
    }
}

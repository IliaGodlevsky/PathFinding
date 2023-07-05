using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Random;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [MediumPriority]
    internal sealed class ResizeGraphMenuItem : GraphCreatingMenuItem
    {
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public ResizeGraphMenuItem(IMessenger messenger,
            IGraphAssemble<Graph2D<Vertex>, Vertex> assemble,
            IRandom random,
            IVertexCostFactory costFactory,
            INeighborhoodFactory neighborhoodFactory,
            GraphsPathfindingHistory history)
            : base(messenger, assemble, random, costFactory, neighborhoodFactory, history)
        {

        }

        private void SetGraph(Graph2D<Vertex> graph)
        {
            this.graph = graph;
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
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }

        public override string ToString()
        {
            return Languages.ResizeGraph;
        }
    }
}

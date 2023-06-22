using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.DataAccess.UnitOfWorks;
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
    [LowPriority]
    internal sealed class ResizeGraphMenuItem : GraphCreatingMenuItem
    {
        private GraphModel graph = new();

        public ResizeGraphMenuItem(IMessenger messenger,
            IGraphAssemble<Graph2D<Vertex>, Vertex> assemble,
            IRandom random, IUnitOfWork unitOfWork,
            IVertexCostFactory costFactory,
            INeighborhoodFactory neighborhoodFactory)
            : base(messenger, assemble, random, unitOfWork, costFactory, neighborhoodFactory)
        {

        }

        private void SetGraph(GraphModel graph)
        {
            this.graph = graph;
        }

        public override bool CanBeExecuted()
        {
            return base.CanBeExecuted()
                && (graph.Graph.Width != width || graph.Graph.Length != length)
                && graph.Graph != Graph2D<Vertex>.Empty;
        }

        protected override IEnumerable<ILayer<Graph2D<Vertex>, Vertex>> GetLayers()
        {
            return base.GetLayers().Append(new GraphLayer(graph.Graph));
        }

        public override void RegisterHanlders(IMessenger messenger)
        {
            base.RegisterHanlders(messenger);
            messenger.RegisterData<GraphModel>(this, Tokens.Common, SetGraph);
        }

        public override string ToString()
        {
            return Languages.ResizeGraph;
        }

        protected override GraphModel GraphAction(GraphModel graph)
        {
            var model = unitOfWork.UpdateGraph(graph);
            var info = unitOfWork.InformationRepository.GetBy(i => i.GraphId == model.Id);
            info.Description = model.Graph.ToString();
            unitOfWork.InformationRepository.Update(info);
            return model;
        }
    }
}

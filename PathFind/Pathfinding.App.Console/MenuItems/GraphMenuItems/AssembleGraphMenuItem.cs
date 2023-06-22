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

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [HighestPriority]
    internal sealed class AssembleGraphMenuItem : GraphCreatingMenuItem
    {
        public AssembleGraphMenuItem(IMessenger messenger,
            IGraphAssemble<Graph2D<Vertex>, Vertex> assemble,
            IRandom random, IUnitOfWork unitOfWork,
            IVertexCostFactory costFactory,
            INeighborhoodFactory neighborhoodFactory)
            : base(messenger, assemble, random, unitOfWork, costFactory, neighborhoodFactory)
        {

        }

        public override string ToString()
        {
            return Languages.AssembleGraph;
        }

        protected override GraphModel GraphAction(GraphModel graph)
        {
            var model = unitOfWork.AddGraph(graph.Graph);
            unitOfWork.AddGraphInformation(model.Id, graph.Graph.ToString());
            return model;
        }
    }
}

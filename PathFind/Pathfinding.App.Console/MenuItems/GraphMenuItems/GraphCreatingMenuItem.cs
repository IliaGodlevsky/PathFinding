using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.Layers;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using Shared.Random;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    using GraphAssemble = IGraphAssemble<Graph2D<Vertex>, Vertex>;

    internal abstract class GraphCreatingMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        protected readonly IMessenger messenger;
        protected readonly IRandom random;
        protected readonly IVertexCostFactory costFactory;
        protected readonly INeighborhoodFactory neighborhoodFactory;
        protected readonly GraphAssemble assemble;
        protected readonly GraphsPathfindingHistory history;

        protected InclusiveValueRange<int> costRange = new(9, 1);
        protected int width = 0;
        protected int length = 0;
        protected int obstaclePercent = 0;

        protected GraphCreatingMenuItem(IMessenger messenger,
            GraphAssemble assemble,
            IRandom random,
            IVertexCostFactory costFactory,
            INeighborhoodFactory neighborhoodFactory,
            GraphsPathfindingHistory history)
        {
            this.history = history;
            this.messenger = messenger;
            this.random = random;
            this.costFactory = costFactory;
            this.neighborhoodFactory = neighborhoodFactory;
            this.assemble = assemble;
        }

        protected void SetCostRange(InclusiveValueRange<int> range) => costRange = range;

        protected void SetObstaclePercent(int percent) => obstaclePercent = percent;

        protected void SetGraphParams((int Width, int Length) parametres)
        {
            width = parametres.Width;
            length = parametres.Length;
        }

        public void Execute()
        {
            var layers = GetLayers();
            var graph = assemble.AssembleGraph(layers, width, length);
            history.Add(graph, new GraphPathfindingHistory());
            messenger.SendData(costRange, Tokens.AppLayout);
            messenger.SendData(graph, Tokens.AppLayout, Tokens.Main, Tokens.Common);
        }

        public virtual bool CanBeExecuted()
        {
            return Constants.GraphWidthValueRange.Contains(width)
                && Constants.GraphLengthValueRange.Contains(length);
        }

        protected virtual IEnumerable<ILayer<Graph2D<Vertex>, Vertex>> GetLayers()
        {
            // The order of the layers shouldn't be changed
            yield return new NeighborhoodLayer<Graph2D<Vertex>, Vertex>(neighborhoodFactory);
            yield return new VertexCostLayer<Graph2D<Vertex>, Vertex>(costFactory, costRange, random);
            yield return new ObstacleLayer<Graph2D<Vertex>, Vertex>(random, obstaclePercent);
        }

        public virtual void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterData<int>(this, Tokens.Graph, SetObstaclePercent);
            messenger.RegisterData<(int Width, int Length)>(this, Tokens.Graph, SetGraphParams);
            messenger.RegisterData<InclusiveValueRange<int>>(this, Tokens.Graph, SetCostRange);
        }
    }
}

using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages.DataMessages;
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

        protected InclusiveValueRange<int> costRange = new(9, 1);
        protected int width = 0;
        protected int length = 0;
        protected int obstaclePercent = 0;

        protected GraphCreatingMenuItem(IMessenger messenger, 
            GraphAssemble assemble,
            IRandom random,
            IVertexCostFactory costFactory, 
            INeighborhoodFactory neighborhoodFactory)
        {
            this.messenger = messenger;
            this.random = random;
            this.costFactory = costFactory;
            this.neighborhoodFactory = neighborhoodFactory;
            this.assemble = assemble;
        }

        protected void OnCostRange(DataMessage<InclusiveValueRange<int>> msg) => costRange = msg.Value;

        protected void OnObstaclePercent(DataMessage<int> msg) => obstaclePercent = msg.Value;

        protected void OnGraphParams(DataMessage<(int Width, int Length)> msg)
        {
            width = msg.Value.Width;
            length = msg.Value.Length;
        }

        public void Execute()
        {
            var layers = GetLayers();
            var graph = assemble.AssembleGraph(layers, width, length);
            messenger.SendData(costRange, Tokens.Screen);
            messenger.SendData(graph, Tokens.Screen | Tokens.Main | Tokens.Common);
        }

        public virtual bool CanBeExecuted()
        {
            return Constants.GraphWidthValueRange.Contains(width)
                && Constants.GraphLengthValueRange.Contains(length);
        }

        protected virtual IEnumerable<ILayer<Graph2D<Vertex>, Vertex>> GetLayers()
        {
            yield return new NeighborhoodLayer<Graph2D<Vertex>, Vertex>(neighborhoodFactory);
            yield return new ObstacleLayer<Graph2D<Vertex>, Vertex>(random, obstaclePercent);
            yield return new VertexCostLayer<Graph2D<Vertex>, Vertex>(costFactory, costRange, random);
        }

        public virtual void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterData<int>(this, Tokens.Graph, OnObstaclePercent);
            messenger.RegisterData<(int Width, int Length)>(this, Tokens.Graph, OnGraphParams);
            messenger.RegisterData<InclusiveValueRange<int>>(this, Tokens.Graph, OnCostRange);
        }
    }
}

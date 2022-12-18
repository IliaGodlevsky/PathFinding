using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.Layers;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using Shared.Random;
using System;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    using GraphAssemble = IGraphAssemble<Graph2D<Vertex>, Vertex>;

    internal abstract class GraphCreatingMenuItem : IMenuItem
    {
        protected readonly IMessenger messenger;
        private readonly IRandom random;
        private readonly IVertexCostFactory costFactory;
        private readonly INeighborhoodFactory neighborhoodFactory;

        private GraphAssemble selected;
        private InclusiveValueRange<int> costRange = new InclusiveValueRange<int>(9, 1);
        protected int width = 0;
        protected int length = 0;
        protected int obstaclePercent = 0;

        public abstract int Order { get; }

        protected GraphCreatingMenuItem(IMessenger messenger, IRandom random,
            IVertexCostFactory costFactory, INeighborhoodFactory neighborhoodFactory)
        {
            this.messenger = messenger;
            this.random = random;
            this.costFactory = costFactory;
            this.neighborhoodFactory = neighborhoodFactory;
            this.messenger.Register<ObstaclePercentMessage>(this, OnObstaclePercent);
            this.messenger.Register<GraphParametresMessage>(this, OnGraphParams);
            this.messenger.Register<CostRangeMessage>(this, OnCostRange);
            this.messenger.Register<ChooseGraphAssembleMessage>(this, OnAssembleChosen);
        }

        private void OnAssembleChosen(ChooseGraphAssembleMessage msg) => selected = msg.Assemble;

        private void OnCostRange(CostRangeMessage msg) => costRange = msg.CostRange;

        private void OnObstaclePercent(ObstaclePercentMessage msg) => obstaclePercent = msg.ObstaclePercent;

        private void OnGraphParams(GraphParametresMessage msg)
        {
            width = msg.Width;
            length = msg.Length;
        }

        public void Execute()
        {
            var layers = GetLayers();
            var graph = selected.AssembleGraph(layers, width, length);
            messenger.Send(new CostRangeChangedMessage(costRange));
            messenger.Send(new GraphCreatedMessage(graph), MessageTokens.Screen);
            messenger.Send(new GraphCreatedMessage(graph), MessageTokens.MainViewModel);
            messenger.Send(new GraphCreatedMessage(graph));
        }

        public virtual bool CanBeExecuted() => selected != null && IsGraphSizeSet();

        protected virtual ILayer<Graph2D<Vertex>, Vertex>[] GetLayers()
        {
            return new ILayer<Graph2D<Vertex>, Vertex>[]
            {
                new ObstacleLayer<Graph2D<Vertex>, Vertex>(random, obstaclePercent),
                new NeighborhoodLayer<Graph2D<Vertex>, Vertex>(neighborhoodFactory),
                new VertexCostLayer<Graph2D<Vertex>, Vertex>(costFactory, costRange, random)
            };
        }

        private bool IsGraphSizeSet()
        {
            return Constants.GraphWidthValueRange.Contains(width)
                && Constants.GraphLengthValueRange.Contains(length);
        }
    }
}

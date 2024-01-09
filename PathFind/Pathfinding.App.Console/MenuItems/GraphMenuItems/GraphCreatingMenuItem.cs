using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.Layers;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using Shared.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    using GraphAssemble = IGraphAssemble<Vertex>;

    internal abstract class GraphCreatingMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        protected readonly IMessenger messenger;
        protected readonly IRandom random;
        protected readonly GraphAssemble assemble;
        protected readonly IService service;

        protected InclusiveValueRange<int> costRange = new(9, 1);
        protected int width = 0;
        protected int length = 0;
        protected int obstaclePercent = 0;
        protected INeighborhoodFactory neighborhoodFactory;

        protected GraphCreatingMenuItem(IMessenger messenger,
            GraphAssemble assemble,
            IRandom random,
            IService service)
        {
            this.service = service;
            this.messenger = messenger;
            this.random = random;
            this.assemble = assemble;
        }

        protected void SetNeighbourhood(NeighbourhoodMessage msg)
            => neighborhoodFactory = msg.Factory;

        protected void SetCostRange(CostRangeMessage msg)
            => costRange = msg.Range;

        protected void SetObstaclePercent(ObstaclePercentMessage msg)
            => obstaclePercent = msg.ObstaclePercent;

        protected void SetGraphParams(GraphParamsMessage msg)
        {
            width = msg.Width;
            length = msg.Length;
        }

        public void Execute()
        {
            var layers = GetLayers();
            var graph = assemble.AssembleGraph(layers, width, length);
            int id = service.AddGraph(graph);
            var costRangeMsg = new CostRangeChangedMessage(costRange);
            messenger.Send(costRangeMsg, Tokens.AppLayout);
            var graphMsg = new GraphMessage(graph, id);
            messenger.SendMany(graphMsg, Tokens.Visual,
                Tokens.AppLayout, Tokens.Main, Tokens.Common);
        }

        public virtual bool CanBeExecuted()
        {
            return Constants.GraphWidthValueRange.Contains(width)
                && Constants.GraphLengthValueRange.Contains(length)
                && neighborhoodFactory != null;
        }

        protected virtual IEnumerable<ILayer> GetLayers()
        {
            yield return new NeighborhoodLayer(neighborhoodFactory);
            yield return new VertexCostLayer(costRange, random);
            yield return new ObstacleLayer(random, obstaclePercent);
        }

        public virtual void RegisterHanlders(IMessenger messenger)
        {
            messenger.Register<GraphCreatingMenuItem, ObstaclePercentMessage>(this, Tokens.Graph, SetObstaclePercent);
            messenger.Register<GraphCreatingMenuItem, NeighbourhoodMessage>(this, Tokens.Graph, SetNeighbourhood);
            messenger.Register<GraphCreatingMenuItem, GraphParamsMessage>(this, Tokens.Graph, SetGraphParams);
            messenger.Register<GraphCreatingMenuItem, CostRangeMessage>(this, Tokens.Graph, SetCostRange);
        }
    }
}

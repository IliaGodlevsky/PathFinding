using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DAL.Interface;
using Pathfinding.App.Console.DAL.Models.TransferObjects.Create;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations;
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

    internal abstract class GraphCreatingMenuItem : IConditionedMenuItem, ICanReceiveMessage
    {
        protected readonly IMessenger messenger;
        protected readonly IRandom random;
        protected readonly GraphAssemble assemble;
        protected readonly IService<Vertex> service;
        protected readonly IInput<string> input;

        protected InclusiveValueRange<int> costRange = new(9, 1);
        protected int width = 0;
        protected int length = 0;
        protected int obstaclePercent = 0;
        protected INeighborhoodFactory neighborhoodFactory;
        protected ILayer smoothLayer;
        protected ReturnOptions returnOptions = ReturnOptions.Limit;

        protected GraphCreatingMenuItem(IMessenger messenger,
            GraphAssemble assemble,
            IRandom random,
            IService<Vertex> service, 
            IInput<string> stringInput)
        {
            this.service = service;
            this.messenger = messenger;
            this.random = random;
            this.assemble = assemble;
            this.input = stringInput;
        }

        protected void SetNeighborhood(NeighbourhoodMessage msg)
            => neighborhoodFactory = msg.Factory;

        protected void SetCostRange(CostRangeMessage msg)
            => costRange = msg.Range;

        protected void SetObstaclePercent(ObstaclePercentMessage msg)
            => obstaclePercent = msg.ObstaclePercent;

        protected void SetSmoothLayer(LayerMessage msg)
            => smoothLayer = msg.Layer;

        protected void SetReturnOptions(ReturnOptionsMessage msg)
            => returnOptions = msg.Options;

        protected void SetGraphParams(GraphParamsMessage msg)
        {
            width = msg.Width;
            length = msg.Length;
        }

        public async void Execute()
        {
            var layers = new Layers(GetLayers());
            var graph = assemble.AssembleGraph(layers, width, length);
            string graphName = input.Input("Enter graph name: ");
            var createDto = new GraphCreateDto<Vertex>() { Graph = graph, Name = graphName };
            var costRangeMsg = new CostRangeChangedMessage(costRange);
            messenger.Send(costRangeMsg, Tokens.AppLayout);
            var graphMsg = new GraphMessage(graph, 0, graphName);
            messenger.SendMany(graphMsg, Tokens.AppLayout, Tokens.Main);
            var read = await Task.Run(() => service.AddGraph(createDto));
            graphMsg = new GraphMessage(read);
            messenger.SendMany(graphMsg, Tokens.Visual, Tokens.Common);
        }

        public virtual bool CanBeExecuted()
        {
            return Constants.GraphWidthValueRange.Contains(width)
                && Constants.GraphLengthValueRange.Contains(length)
                && neighborhoodFactory != null;
        }

        protected virtual IEnumerable<ILayer> GetLayers()
        {
            yield return new NeighborhoodLayer(neighborhoodFactory, returnOptions);
            yield return new VertexCostLayer(costRange, random);
            yield return new ObstacleLayer(random, obstaclePercent);
            yield return smoothLayer ?? new Layers();
        }

        public virtual void RegisterHandlers(IMessenger messenger)
        {
            messenger.Register<GraphCreatingMenuItem, ObstaclePercentMessage>(this, Tokens.Graph, SetObstaclePercent);
            messenger.Register<GraphCreatingMenuItem, NeighbourhoodMessage>(this, Tokens.Graph, SetNeighborhood);
            messenger.Register<GraphCreatingMenuItem, ReturnOptionsMessage>(this, Tokens.Graph, SetReturnOptions);
            messenger.Register<GraphCreatingMenuItem, GraphParamsMessage>(this, Tokens.Graph, SetGraphParams);
            messenger.Register<GraphCreatingMenuItem, CostRangeMessage>(this, Tokens.Graph, SetCostRange);
            messenger.Register<GraphCreatingMenuItem, LayerMessage>(this, Tokens.Graph, SetSmoothLayer);
        }
    }
}

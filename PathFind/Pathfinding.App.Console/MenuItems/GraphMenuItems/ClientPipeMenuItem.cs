using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal sealed class ClientPipeMenuItem : IMenuItem
    {
        private readonly IGraphSerializer<Graph2D<Vertex>, Vertex> graphSerializer;
        private readonly IMessenger messenger;
        private readonly ILog log;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public ClientPipeMenuItem(IGraphSerializer<Graph2D<Vertex>, Vertex> graphSerializer, 
            IMessenger messenger, ILog log)
        {
            this.graphSerializer = graphSerializer;
            this.messenger = messenger;
            this.log = log;
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        public bool CanBeExecuted()
        {
            return graph != Graph2D<Vertex>.Empty;
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }

        public void Execute()
        {
            try
            {
                graphSerializer.SendGraphToPipe(graph, Constants.PipeName);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public override string ToString()
        {
            return "Send";
        }
    }
}

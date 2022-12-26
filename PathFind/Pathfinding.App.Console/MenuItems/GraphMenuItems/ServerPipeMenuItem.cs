using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    internal sealed class ServerPipeMenuItem : IMenuItem
    {
        private readonly IGraphSerializer<Graph2D<Vertex>, Vertex> serializer;
        private readonly IMessenger messenger;
        private readonly ILog log;

        public ServerPipeMenuItem(IGraphSerializer<Graph2D<Vertex>, Vertex> serializer, 
            IMessenger messenger, ILog log)
        {
            this.serializer = serializer;
            this.messenger = messenger;
            this.log = log;
        }

        public bool CanBeExecuted() => true;

        public void Execute()
        {
            try
            {
                var graph = serializer.RecieveGraphFromPipe(Constants.PipeName);
                var range = graph.First().Cost.CostRange;
                messenger.Send(new CostRangeChangedMessage(range));
                messenger.Send(new GraphCreatedMessage(graph), MessageTokens.Screen);
                messenger.Send(new GraphCreatedMessage(graph), MessageTokens.MainViewModel);
                messenger.Send(new GraphCreatedMessage(graph));
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public override string ToString()
        {
            return "Recieve";
        }
    }
}

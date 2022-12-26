using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using Shared.Primitives.Attributes;
using System;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [Order(6)]
    internal sealed class LoadGraphMenuItem : IMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IGraphSerializationModule<Graph2D<Vertex>, Vertex> module;
        private readonly ILog log;

        public LoadGraphMenuItem(IMessenger messenger, 
            IGraphSerializationModule<Graph2D<Vertex>, Vertex> module, ILog log)
        {
            this.messenger = messenger;
            this.module = module;
            this.log = log;
        }

        public bool CanBeExecuted() => true;

        public void Execute()
        {
            try
            {
                var graph = module.LoadGraph();
                var range = graph.First().Cost.CostRange;
                messenger.Send(new CostRangeChangedMessage(range));
                messenger.Send(new GraphCreatedMessage(graph), MessageTokens.Screen);
                messenger.Send(new GraphCreatedMessage(graph), MessageTokens.MainUnit);
                messenger.Send(new GraphCreatedMessage(graph));
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public override string ToString()
        {
            return Languages.LoadGraph;
        }
    }
}

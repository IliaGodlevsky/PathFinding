using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
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
        private readonly IPathInput input;
        private readonly IGraphSerializer<Graph2D<Vertex>, Vertex> serializer;
        private readonly ILog log;

        public LoadGraphMenuItem(IMessenger messenger,
            IPathInput input,
            IGraphSerializer<Graph2D<Vertex>, Vertex> serializer, ILog log)
        {
            this.serializer = serializer;
            this.messenger = messenger;
            this.input = input;
            this.log = log;
        }

        public void Execute()
        {
            try
            {
                string path = input.InputLoadPath();
                var graph = serializer.LoadGraphFromFile(path);
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

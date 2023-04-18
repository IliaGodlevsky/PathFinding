using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages.DataMessages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;
using System.Runtime.CompilerServices;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [MediumPriority]
    internal sealed class SendGraphMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly AddressInput input;
        private readonly IGraphSerializer<Graph2D<Vertex>, Vertex> serializer;
        private readonly ILog log;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public SendGraphMenuItem(IMessenger messenger,
            AddressInput input,
            IGraphSerializer<Graph2D<Vertex>, Vertex> serializer,
            ILog log)
        {
            this.messenger = messenger;
            this.input = input;
            this.serializer = serializer;
            this.log = log;
        }

        public bool CanBeExecuted() => graph != Graph2D<Vertex>.Empty;

        public async void Execute()
        {
            try
            {
                var address = input.InputAddress();
                await serializer.SendGraphAsync(graph, address.Host, address.Port);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void OnGraphCreated(DataMessage<Graph2D<Vertex>> msg)
        {
            graph = msg.Value;
        }

        public override string ToString()
        {
            return "Send graph";
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, OnGraphCreated);
        }
    }
}

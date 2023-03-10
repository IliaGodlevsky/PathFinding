﻿using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages.DataMessages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class SendGraphMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IGraphSerializer<Graph2D<Vertex>, Vertex> serializer;
        private readonly IMessenger messenger;
        private readonly IInput<string> input;
        private readonly ILog log;

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public SendGraphMenuItem(IGraphSerializer<Graph2D<Vertex>, Vertex> serializer,
            IMessenger messenger, IInput<string> input, ILog log)
        {
            this.serializer = serializer;
            this.messenger = messenger;
            this.input = input;
            this.log = log;
        }

        public bool CanBeExecuted()
        {
            return graph != Graph2D<Vertex>.Empty;
        }

        private void OnGraphCreated(DataMessage<Graph2D<Vertex>> msg)
        {
            graph = msg.Value;
        }

        public async void Execute()
        {
            try
            {
                using (Cursor.UseCurrentPositionWithClean())
                {
                    string pipeName = input.Input(Languages.InputPipeMsg);
                    string serverName = input.Input(Languages.InputServerNameMsg);
                    await serializer.SaveGraphToPipeAsync(graph, pipeName, serverName);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public override string ToString()
        {
            return Languages.SendGraph;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, OnGraphCreated);
        }
    }
}

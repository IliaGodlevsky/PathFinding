﻿using GalaSoft.MvvmLight.Messaging;
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

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [Order(7)]
    internal sealed class SaveGraphMenuItem : IMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IPathInput input;
        private readonly IGraphSerializer<Graph2D<Vertex>, Vertex> serializer;
        private readonly ILog log;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public SaveGraphMenuItem(IMessenger messenger,
            IPathInput input,
            IGraphSerializer<Graph2D<Vertex>, Vertex> serializer,
            ILog log)
        {
            this.messenger = messenger;
            this.input = input;
            this.serializer = serializer;
            this.log = log;
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        public bool CanBeExecuted() => graph != Graph2D<Vertex>.Empty;

        public async void Execute()
        {
            try
            {
                string path = input.InputSavePath();
                await serializer.SaveGraphToFileAsync(graph, path);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }

        public override string ToString()
        {
            return Languages.SaveGraph;
        }
    }
}

﻿using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Extensions;
using Pathfinding.Logging.Interface;
using System;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class LoadOnlyGraphMenuItem : IMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IInput<string> input;
        private readonly ISerializer<Graph2D<Vertex>> serializer;
        private readonly GraphsPathfindingHistory history;
        private readonly ILog log;

        public LoadOnlyGraphMenuItem(IMessenger messenger,
            IFilePathInput input,
            GraphsPathfindingHistory history,
            ISerializer<Graph2D<Vertex>> serializer, ILog log)
        {
            this.history = history;
            this.serializer = serializer;
            this.messenger = messenger;
            this.input = input;
            this.log = log;
        }

        public void Execute()
        {
            try
            {
                var path = input.Input();
                var graph = serializer.DeserializeFromFile(path);
                history.Add(graph, new GraphPathfindingHistory());
                if (history.Count == 1)
                {
                    var costRange = graph.First().Cost.CostRange;
                    messenger.SendData(costRange, Tokens.AppLayout);
                    messenger.SendData(graph, Tokens.AppLayout, Tokens.Main, Tokens.Common);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public override string ToString()
        {
            return "Load graph only";
        }
    }
}

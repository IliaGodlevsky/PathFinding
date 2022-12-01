﻿using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.Logging.Interface;
using System;
using System.Linq;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(1)]
    internal sealed class GraphLoadViewModel : SafeViewModel, IDisposable
    {
        private readonly IMessenger messenger;
        private readonly IGraphSerializationModule<Graph2D<Vertex>, Vertex> module;

        public GraphLoadViewModel(IGraphSerializationModule<Graph2D<Vertex>, Vertex> module, 
            IMessenger messenger, ILog log)
            : base(log)
        {
            this.messenger = messenger;
            this.module = module;
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [MenuItem(MenuItemsNames.LoadGraph, 0)]
        private void LoadGraph()
        {
            Graph2D<Vertex> graph;
            using (Cursor.CleanUpAfter())
            {
                graph = module.LoadGraph();
            }
            var range = graph.First().Cost.CostRange;
            messenger.Send(new CostRangeChangedMessage(range));
            messenger.Send(new GraphCreatedMessage(graph), MessageTokens.Screen);
            messenger.Send(new GraphCreatedMessage(graph), MessageTokens.MainViewModel);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}

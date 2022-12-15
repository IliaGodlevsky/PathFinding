﻿using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Modules.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Visualization.Extensions;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    internal sealed class ClearColorsMenuItem : IMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public int Order => 5;

        public ClearColorsMenuItem(IMessenger messenger, IPathfindingRangeBuilder<Vertex> rangeBuilder)
        {
            this.messenger = messenger;
            this.rangeBuilder = rangeBuilder;
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        public bool CanBeExecuted() => graph != Graph2D<Vertex>.Empty;

        public void Execute()
        {
            messenger.Send(PathfindingStatisticsMessage.Empty);
            graph.RestoreVerticesVisualState();
            rangeBuilder.Range.RestoreVerticesVisualState();
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }

        public override string ToString()
        {
            return Languages.ClearColors;
        }
    }
}

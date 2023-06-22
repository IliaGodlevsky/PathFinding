using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Visualization.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingHistoryUnit : Unit, ICanRecieveMessage
    {
        private readonly List<ICoordinate> visited = new();
        private readonly IMessenger messenger;

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;
        private bool isHistoryApplied = true;

        public PathfindingHistoryUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned,
            IMessenger messenger)
            : base(menuItems, conditioned)
        {
            this.messenger = messenger;
        }

        private void VisualizeHistory(long key)
        {
            graph.RestoreVerticesVisualState();
            var msg = new AskVisualizationSetMessage() { Id = key };
            messenger.Send(msg, Tokens.Storage);
            var set = msg.Response;
            graph.ApplyCosts(set.Costs);
            set.Visualize(graph);
        }

        private void SetIsApplied(bool isApplied)
        {
            isHistoryApplied = isApplied;
        }

        private void SetGraph(Graph2D<Vertex> graph)
        {
            this.graph = graph;
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            visited.Add(e.Current);
        }

        private bool IsHistoryApplied() => isHistoryApplied;

        private void SubscribeOnHistory(PathfindingProcess algorithm)
        {
            var obstacles = graph.GetObstaclesCoordinates();
            algorithm.VertexVisited += OnVertexVisited;
            algorithm.Finished += OnAlgorithmFinished;
        }

        private void OnAlgorithmFinished(object sender, EventArgs e)
        {
            var message = new VisitedMessage { Coordinates = visited };
            messenger.SendData(message, Tokens.Storage);
            visited.Clear();
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = Tokens.Bind(IsHistoryApplied, Tokens.History);
            messenger.RegisterData<PathfindingProcess>(this, token, SubscribeOnHistory);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
            messenger.RegisterData<bool>(this, Tokens.History, SetIsApplied);
            messenger.RegisterData<long>(this, token, VisualizeHistory);
        }
    }
}
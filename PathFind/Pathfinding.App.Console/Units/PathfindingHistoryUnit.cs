using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.History;
using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Messages.DataMessages;
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
        private readonly IMessenger messenger;
        private readonly IHistoryRepository<IHistoryVolume<ICoordinate>> repository;
        private readonly Dictionary<Guid, IReadOnlyList<int>> pricesHistory = new();

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;
        private bool isHistoryApplied = false;

        public PathfindingHistoryUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned,
            IHistoryRepository<IHistoryVolume<ICoordinate>> repository,
            IMessenger messenger)
            : base(menuItems, conditioned)
        {
            this.messenger = messenger;
            this.repository = repository;
        }

        private void OnHistoryPage(DataMessage<Guid> msg)
        {
            graph.RestoreVerticesVisualState();
            graph.ApplyCosts(pricesHistory[msg.Value]);
            repository.VisualizeHistory(msg.Value, graph);
        }

        private void OnHistoryApplied(DataMessage<bool> msg)
        {
            isHistoryApplied = msg.Value;
        }

        private void OnGraphCreated(DataMessage<Graph2D<Vertex>> msg)
        {
            graph = msg.Value;
            ClearHistory();
        }

        private void ClearHistory()
        {
            repository.Clear();
            pricesHistory.Clear();
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            if (sender is IHistoryPageKey key)
            {
                repository.AddVisited(key.Id, e.Current);
            }
        }

        private bool IsHistoryApplied() => isHistoryApplied;

        private void OnRangeChosen(AlgorithmMessage<IPathfindingRange<Vertex>> msg)
        {
            repository.AddPathfindingRange(msg.Algorithm.Id, msg.Value);
        }

        private void OnPathFound(AlgorithmMessage<IGraphPath> msg)
        {
            repository.AddPath(msg.Algorithm.Id, msg.Value);
        }

        private void OnSubscribeOnHistory(DataMessage<PathfindingProcess> msg)
        {
            repository.AddObstacles(msg.Value.Id, graph.GetObstaclesCoordinates());
            pricesHistory.Add(msg.Value.Id, graph.GetCosts());
            msg.Value.VertexVisited += OnVertexVisited;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = ConditionToken.Create(IsHistoryApplied, Tokens.History);
            messenger.RegisterAlgorithmData<IPathfindingRange<Vertex>>(this, token, OnRangeChosen);
            messenger.RegisterAlgorithmData<IGraphPath>(this, token, OnPathFound);
            messenger.RegisterData<PathfindingProcess>(this, token, OnSubscribeOnHistory);
            messenger.RegisterGraph(this, Tokens.Common, OnGraphCreated);
            messenger.RegisterData<bool>(this, Tokens.History, OnHistoryApplied);
            messenger.RegisterData<Guid>(this, Tokens.History, OnHistoryPage);
            messenger.Register<ClearHistoryMessage>(this, _ => ClearHistory());
        }
    }
}
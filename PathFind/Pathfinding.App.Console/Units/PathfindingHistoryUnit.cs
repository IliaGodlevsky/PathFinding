using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.History;
using Pathfinding.AlgorithmLib.History.Interface;
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
        private readonly IHistoryRepository<IHistoryVolume<ICoordinate>> repository;
        private readonly Dictionary<Guid, IReadOnlyList<int>> pricesHistory = new();

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;
        private bool isHistoryApplied = true;

        public PathfindingHistoryUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned,
            IHistoryRepository<IHistoryVolume<ICoordinate>> repository)
            : base(menuItems, conditioned)
        {
            this.repository = repository;
        }

        private void VisualizeHistory(Guid key)
        {
            graph.RestoreVerticesVisualState();
            graph.ApplyCosts(pricesHistory[key]);
            repository.VisualizeHistory(key, graph);
        }

        private void SetIsApplied(bool isApplied)
        {
            isHistoryApplied = isApplied;
        }

        private void SetGraph(Graph2D<Vertex> graph)
        {
            this.graph = graph;
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

        private void AddRange((PathfindingProcess Algorithm, IPathfindingRange<Vertex> Range) value)
        {
            repository.AddPathfindingRange(value.Algorithm.Id, value.Range);
        }

        private void AddPath((PathfindingProcess Algorithm, IGraphPath Path) value)
        {
            repository.AddPath(value.Algorithm.Id, value.Path);
        }

        private void SubscribeOnHistory(PathfindingProcess algorithm)
        {
            repository.AddObstacles(algorithm.Id, graph.GetObstaclesCoordinates());
            pricesHistory.Add(algorithm.Id, graph.GetCosts());
            algorithm.VertexVisited += OnVertexVisited;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = Tokens.Bind(IsHistoryApplied, Tokens.History);
            messenger.RegisterAlgorithmData<IPathfindingRange<Vertex>>(this, token, AddRange);
            messenger.RegisterAlgorithmData<IGraphPath>(this, token, AddPath);
            messenger.RegisterData<PathfindingProcess>(this, token, SubscribeOnHistory);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
            messenger.RegisterData<bool>(this, Tokens.History, SetIsApplied);
            messenger.RegisterData<Guid>(this, token, VisualizeHistory);
            messenger.RegisterAction<ClearHistoryMessage>(this, Tokens.History, ClearHistory);
        }
    }
}
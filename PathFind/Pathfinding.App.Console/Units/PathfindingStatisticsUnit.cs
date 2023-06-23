using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Notes;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingStatisticsUnit : Unit, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly PathfindingHistory history;
        private readonly Stopwatch timer = new();

        private bool isStatisticsApplied = true;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;
        private int visited = 0;

        public PathfindingStatisticsUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned,
            IMessenger messenger,
            PathfindingHistory history)
            : base(menuItems, conditioned)
        {
            this.messenger = messenger;
            this.history = history;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = Tokens.Bind(IsStatisticsApplied, Tokens.Statistics);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
            messenger.RegisterData<PathfindingProcess>(this, token, SubscribeOnStatistics);
            messenger.RegisterAlgorithmData<IGraphPath>(this, Tokens.Statistics, OnPathFound);
            messenger.RegisterData<bool>(this, Tokens.Statistics, SetIsApplied);
        }

        private bool IsStatisticsApplied() => isStatisticsApplied;

        private void SubscribeOnStatistics(PathfindingProcess algorithm)
        {
            algorithm.VertexVisited += OnVertexVisited;
            algorithm.Finished += (s, e) => timer.Stop();
            algorithm.Started += (s, e) => timer.Restart();
            algorithm.Interrupted += (s, e) => timer.Stop();
            algorithm.Paused += (s, e) => timer.Stop();
            algorithm.Resumed += (s, e) => timer.Start();
        }

        private void SetIsApplied(bool isApplied)
        {
            isStatisticsApplied = isApplied;
        }

        private void OnPathFound((PathfindingProcess Process, IGraphPath Path) value)
        {
            var note = GetStatistics(value.Process);
            if (IsStatisticsApplied())
            {
                if (value.Path.Count > 0)
                {
                    note = GetStatistics(value.Path, value.Process);
                }
                visited = 0;
                messenger.SendData(note.ToString(), Tokens.AppLayout);
            }
            history.History[graph].Statistics.Add(value.Process.Id, note);
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            visited++;
            var statistics = GetStatistics((PathfindingProcess)sender);
            messenger.SendData(statistics.ToString(), Tokens.AppLayout);
        }

        private void SetGraph(Graph2D<Vertex> graph)
        {
            this.graph = graph;
        }

        private StatisticsNote GetStatistics(IGraphPath path, PathfindingProcess algorithm)
        {
            return new()
            {
                AlgorithmName = algorithm.ToString(),
                Elapsed = timer.Elapsed,
                VisitedVertices = visited,
                Steps = path.Count,
                Cost = path.Cost
            };
        }

        private StatisticsNote GetStatistics(PathfindingProcess algorithm)
        {
            return GetStatistics(NullGraphPath.Instance, (PathfindingProcess)algorithm);
        }
    }
}
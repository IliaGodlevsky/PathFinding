using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Interface.Extensions;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.Notes;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingStatisticsUnit : Unit, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly Stopwatch timer = new();

        private bool isStatisticsApplied = true;
        private int visited = 0;
        private Statistics statistics = Statistics.Empty;

        public PathfindingStatisticsUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned,
            IMessenger messenger)
            : base(menuItems, conditioned)
        {
            this.messenger = messenger;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = Tokens.Bind(IsStatisticsApplied, Tokens.Statistics);
            messenger.RegisterData<PathfindingProcess>(this, token, SubscribeOnStatistics);
            messenger.RegisterData<Statistics>(this, Tokens.Statistics, SetStatistics);
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

        private void SetStatistics(Statistics stats)
        {
            statistics = stats;
        }

        private void OnPathFound((PathfindingProcess Process, IGraphPath Path) value)
        {
            var note = GetStatistics(value.Path);
            note.ResultStatus = AlgorithmResultStatus.Succeeded;
            if (value.Path.IsEmpty())
            {
                note.ResultStatus = AlgorithmResultStatus.Failed;
            }
            visited = 0;
            statistics = Statistics.Empty;
            if (IsStatisticsApplied())
            {
                messenger.SendData(note.ToString(), Tokens.AppLayout);
            }
            messenger.SendAlgorithmData(value.Process, note, Tokens.History);
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            visited++;
            messenger.SendData(GetStatistics().ToString(), Tokens.AppLayout);
        }

        private Statistics GetStatistics(IGraphPath path)
        {
            var stats = new Statistics();
            stats.Algorithm = statistics.Algorithm;
            stats.ResultStatus = statistics.ResultStatus;
            if (IsStatisticsApplied())
            {
                stats.StepRule = statistics.StepRule;
                stats.Heuristics = statistics.Heuristics;
                stats.Elapsed = timer.Elapsed;
                stats.Visited = visited;
                stats.Spread = statistics.Spread;
            }
            if (!path.IsEmpty() && IsStatisticsApplied())
            {
                stats.Steps = path.Count;
                stats.Cost = path.Cost;
            }
            return stats;
        }

        private Statistics GetStatistics()
        {
            return GetStatistics(NullGraphPath.Instance);
        }
    }
}
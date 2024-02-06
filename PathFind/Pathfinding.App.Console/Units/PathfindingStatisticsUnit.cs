using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Core.Interface.Extensions;
using Pathfinding.AlgorithmLib.Core.NullObjects;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model.Notes;
using System;
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
            IMessenger messenger)
            : base(menuItems)
        {
            this.messenger = messenger;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = Tokens.Statistics.Bind(IsStatisticsApplied);
            messenger.Register<PathfindingStatisticsUnit, AlgorithmMessage>(this, token, SubscribeOnStatistics);
            messenger.Register<PathfindingStatisticsUnit, StatisticsMessage>(this, Tokens.Statistics, SetStatistics);
            messenger.Register<PathfindingStatisticsUnit, PathFoundMessage>(this, Tokens.Statistics, OnPathFound);
            messenger.Register<PathfindingStatisticsUnit, IsAppliedMessage>(this, Tokens.Statistics, SetIsApplied);
            messenger.Register<PathfindingStatisticsUnit, AlgorithmDelayMessage>(this, token, SetAnimationDelay);
        }

        private bool IsStatisticsApplied() => isStatisticsApplied;

        private void SubscribeOnStatistics(AlgorithmMessage msg)
        {
            var algorithm = msg.Algorithm;
            algorithm.VertexVisited += OnVertexVisited;
            algorithm.Interrupted += OnInterrupted;
            algorithm.Finished += (s, e) => timer.Stop();
            algorithm.Started += OnStarted;
            algorithm.Paused += (s, e) => timer.Stop();
            algorithm.Resumed += (s, e) => timer.Start();
        }

        private void OnStarted(object sender, EventArgs e)
        {
            statistics.ResultStatus = AlgorithmStatuses.Started;
            timer.Restart();
        }

        private void OnInterrupted(object sender, EventArgs e)
        {
            statistics.ResultStatus = Languages.Interrupted;
            timer.Stop();
        }

        private void SetIsApplied(IsAppliedMessage msg)
        {
            isStatisticsApplied = msg.IsApplied;
        }

        private void SetAnimationDelay(AlgorithmDelayMessage message)
        {
            statistics.AlgorithmSpeed = message.Delay;
        }

        private void SetStatistics(StatisticsMessage msg)
        {
            statistics = msg.Statistics with { AlgorithmSpeed = statistics.AlgorithmSpeed };
        }

        private void OnPathFound(PathFoundMessage message)
        {
            var note = GetStatistics(message.Path);
            if (note.ResultStatus != AlgorithmStatuses.Interrupted)
            {
                note.ResultStatus = AlgorithmStatuses.Succeeded;
            }
            if (message.Path.IsEmpty()
                && note.ResultStatus != AlgorithmStatuses.Interrupted)
            {
                note.ResultStatus = AlgorithmStatuses.Failed;
            }
            if (IsStatisticsApplied())
            {
                var statLineMsg = new StatisticsLineMessage(note.ToString());
                messenger.Send(statLineMsg, Tokens.AppLayout);
            }
            var msg = new StatisticsMessage(note);
            messenger.Send(msg, Tokens.History);
            visited = 0;
            statistics = Statistics.Empty;
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            visited++;
            string line = GetStatistics().ToString();
            var msg = new StatisticsLineMessage(line);
            messenger.Send(msg, Tokens.AppLayout);
        }

        private Statistics GetStatistics(IGraphPath path)
        {
            var stats = new Statistics(statistics.Algorithm)
            {
                ResultStatus = statistics.ResultStatus,
                StepRule = statistics.StepRule,
                Heuristics = statistics.Heuristics
            };
            if (IsStatisticsApplied())
            {
                stats.AlgorithmSpeed = statistics.AlgorithmSpeed;
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
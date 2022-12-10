using Autofac.Features.AttributeFilters;
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class PathfindingStatisticsUnit : Unit
    {
        private readonly IMessenger messenger;
        private readonly Stopwatch timer = new();
        private readonly Dictionary<Guid, string> statistics = new();

        private bool isStatisticsApplied = false;
        private int visited = 0;

        public PathfindingStatisticsUnit([KeyFilter(typeof(PathfindingStatisticsUnit))]IReadOnlyCollection<IMenuItem> menuItems, 
            IMessenger messenger) : base(menuItems)
        {
            this.messenger = messenger;
            this.messenger.Register<SubscribeOnStatisticsMessage>(this, OnSusbcribe);
            this.messenger.Register<PathFoundMessage>(this, OnPathFound);
            this.messenger.Register<ApplyStatisticsMessage>(this, ApplyStatistics);
            this.messenger.Register<ClearHistoryMessage>(this, ClearHistory);
            this.messenger.Register<HistoryPageMessage>(this, ShowStatistics);
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        private void OnSusbcribe(SubscribeOnStatisticsMessage message)
        {
            if (isStatisticsApplied)
            {
                message.Algorithm.VertexVisited += OnVertexVisited;
                message.Algorithm.Finished += (s, e) => timer.Stop();
                message.Algorithm.Started += (s, e) => timer.Restart();
                message.Algorithm.Interrupted += (s, e) => timer.Stop();
                message.Algorithm.Paused += (s, e) => timer.Stop();
                message.Algorithm.Resumed += (s, e) => timer.Start();
            }
            else
            {
                message.Algorithm.VertexVisited += OnVertexVisitedUnappled;
            }
        }

        private void ClearHistory(ClearHistoryMessage msg) => statistics.Clear();

        private void OnGraphCreated(GraphCreatedMessage msg) => statistics.Clear();

        private void ApplyStatistics(ApplyStatisticsMessage message) => isStatisticsApplied = message.IsApplied;

        private void ShowStatistics(HistoryPageMessage msg)
        {
            var stats = statistics[msg.PageKey];
            messenger.Send(new PathfindingStatisticsMessage(stats));
        }

        private void OnPathFound(PathFoundMessage message)
        {
            string statistics = GetStatisticsUnApplied(message.Algorithm);
            if (isStatisticsApplied)
            {
                statistics = message.Path.Count > 0
                    ? GetStatistics(message.Path, message.Algorithm)
                    : GetStatistics(message.Algorithm);
                visited = 0;
            }
            this.statistics[message.Algorithm.Id] = statistics;
            messenger.Send(new PathfindingStatisticsMessage(statistics));
            messenger.Send(new AlgorithmFinishedMessage(message.Algorithm, statistics));
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            visited++;
            var statistics = GetStatistics((PathfindingProcess)sender);
            messenger.Send(new PathfindingStatisticsMessage(statistics));
        }

        private void OnVertexVisitedUnappled(object sender, PathfindingEventArgs e)
        {
            string statistics = GetStatisticsUnApplied((PathfindingProcess)sender);
            messenger.Send(new PathfindingStatisticsMessage(statistics));
        }

        public string GetStatistics(PathfindingProcess algorithm)
        {
            var time = timer.Elapsed.ToString(@"mm\:ss\.fff");
            string pathfindingInfo = string.Format(MessagesTexts.InProcessStatisticsFormat, visited);
            return string.Join("\t", algorithm, time, pathfindingInfo);
        }

        public string GetStatisticsUnApplied(PathfindingProcess algorithm) => algorithm.ToString();

        public string GetStatistics(IGraphPath path, PathfindingProcess algorithm)
        {
            var time = timer.Elapsed.ToString(@"mm\:ss\.fff");
            string pathfindingInfo = string.Format(MessagesTexts.PathfindingStatisticsFormat, 
                path.Count, path.Cost, visited);
            return string.Join("\t", algorithm, time, pathfindingInfo);
        }
    }
}
using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingStatisticsUnit : Unit, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly Stopwatch timer = new();

        private bool isStatisticsApplied = false;
        private int visited = 0;

        public PathfindingStatisticsUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned,
            IMessenger messenger)
            : base(menuItems, conditioned)
        {
            this.messenger = messenger;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = ConditionToken.Create(IsStatisticsApplied, MessageTokens.StatisticsUnit);
            messenger.Register<SubscribeOnStatisticsMessage>(this, token, OnSusbcribe);
            messenger.Register<PathFoundMessage>(this, MessageTokens.StatisticsUnit, OnPathFound);
            messenger.Register<ApplyStatisticsMessage>(this, ApplyStatistics);
        }

        private bool IsStatisticsApplied() => isStatisticsApplied;

        private void OnSusbcribe(SubscribeOnStatisticsMessage message)
        {
            message.Algorithm.VertexVisited += OnVertexVisited;
            message.Algorithm.Finished += (s, e) => timer.Stop();
            message.Algorithm.Started += (s, e) => timer.Restart();
            message.Algorithm.Interrupted += (s, e) => timer.Stop();
            message.Algorithm.Paused += (s, e) => timer.Stop();
            message.Algorithm.Resumed += (s, e) => timer.Start();
        }

        private void ApplyStatistics(ApplyStatisticsMessage message)
        {
            isStatisticsApplied = message.IsApplied;
        }

        private void OnPathFound(PathFoundMessage message)
        {
            string stats = message.Algorithm.ToString();
            if (isStatisticsApplied)
            {
                stats = message.Path.Count > 0
                    ? GetStatistics(message.Path, message.Algorithm)
                    : GetStatistics(message.Algorithm);
                visited = 0;
                messenger.Send(new PathfindingStatisticsMessage(stats));
            }
            messenger.Send(new AlgorithmFinishedMessage(message.Algorithm, stats));
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            visited++;
            var statistics = GetStatistics((PathfindingProcess)sender);
            messenger.Send(new PathfindingStatisticsMessage(statistics));
        }

        private string GetStatistics(PathfindingProcess algorithm)
        {
            return GetStatistics(Languages.InProcessStatisticsFormat, algorithm, visited);
        }

        private string GetStatistics(IGraphPath path, PathfindingProcess algorithm)
        {
            return GetStatistics(Languages.PathfindingStatisticsFormat, algorithm, path.Count, path.Cost, visited);
        }

        private string GetStatistics(string format, PathfindingProcess algorithm, params object[] values)
        {
            var time = timer.Elapsed.ToString(@"mm\:ss\.fff");
            string algorithmName = algorithm.ToString().PadRight(totalWidth: 20);
            string pathfindingInfo = string.Format(format, values);
            return string.Join("\t", algorithmName, time, pathfindingInfo);
        }
    }
}
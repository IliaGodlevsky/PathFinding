using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages.DataMessages;
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
            var token = ConditionToken.Create(IsStatisticsApplied, Tokens.Statistics);
            messenger.RegisterData<PathfindingProcess>(this, token, OnSusbcribe);
            messenger.RegisterAlgorithmData<IGraphPath>(this, Tokens.Statistics, OnPathFound);
            messenger.RegisterData<bool>(this, Tokens.Statistics, RecieveApplyStatistics);
        }

        private bool IsStatisticsApplied() => isStatisticsApplied;

        private void OnSusbcribe(DataMessage<PathfindingProcess> message)
        {
            message.Value.VertexVisited += OnVertexVisited;
            message.Value.Finished += (s, e) => timer.Stop();
            message.Value.Started += (s, e) => timer.Restart();
            message.Value.Interrupted += (s, e) => timer.Stop();
            message.Value.Paused += (s, e) => timer.Stop();
            message.Value.Resumed += (s, e) => timer.Start();
        }

        private void RecieveApplyStatistics(DataMessage<bool> msg)
        {
            isStatisticsApplied = msg.Value;
        }

        private void OnPathFound(AlgorithmMessage<IGraphPath> msg)
        {
            string stats = msg.Algorithm.ToString();
            if (isStatisticsApplied)
            {
                stats = msg.Value.Count > 0
                    ? GetStatistics(msg.Value, msg.Algorithm)
                    : GetStatistics(msg.Algorithm);
                visited = 0;
                messenger.SendData(stats, Tokens.Screen);
            }
            var message = new AlgorithmMessage<string>(msg.Algorithm, stats);
            messenger.Send(message, Tokens.History);
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            visited++;
            var statistics = GetStatistics((PathfindingProcess)sender);
            messenger.SendData(statistics, Tokens.Screen);
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
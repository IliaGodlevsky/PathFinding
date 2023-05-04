using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages.DataMessages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Notes;
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
            var algorithm = msg.Algorithm;
            var note = GetStatistics(algorithm);
            if (IsStatisticsApplied())
            {
                if (msg.Value.Count > 0)
                {
                    note = GetStatistics(msg.Value, algorithm);
                }
                visited = 0;
                messenger.SendData(note.ToString(), Tokens.Screen);
            }
            var message = new AlgorithmMessage<StatisticsNote>(algorithm, note);
            messenger.Send(message, Tokens.History);
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            visited++;
            var statistics = GetStatistics((PathfindingProcess)sender);
            messenger.SendData(statistics.ToString(), Tokens.Screen);
        }

        private StatisticsNote GetStatistics(PathfindingProcess algorithm)
        {
            return new ()
            {
                AlgorithmName = algorithm.ToString(),
                Elapsed = timer.Elapsed,
                VisitedVertices = visited
            };
        }

        private HistoryNote GetStatistics(IGraphPath path, PathfindingProcess algorithm)
        {
            var statistics = GetStatistics(algorithm);
            return new ()
            {
                AlgorithmName = statistics.AlgorithmName,
                Elapsed = statistics.Elapsed,
                VisitedVertices = statistics.VisitedVertices,
                Cost = path.Cost,
                Steps = path.Count
            };
        }
    }
}
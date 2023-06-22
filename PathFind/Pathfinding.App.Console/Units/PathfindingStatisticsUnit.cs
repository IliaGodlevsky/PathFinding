using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
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
        private string algorithmName = string.Empty;

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
            messenger.RegisterData<IGraphPath>(this, Tokens.Statistics, OnPathFound);
            messenger.RegisterData<bool>(this, Tokens.Statistics, SetIsApplied);
        }

        private bool IsStatisticsApplied() => isStatisticsApplied;

        private void SubscribeOnStatistics(PathfindingProcess algorithm)
        {
            algorithmName = algorithm.ToString();
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

        private void OnPathFound(IGraphPath path)
        {
            var note = GetStatistics(algorithmName);
            if (IsStatisticsApplied())
            {
                if (path.Count > 0)
                {
                    note = GetStatistics(path, algorithmName);
                }
                visited = 0;
                algorithmName = string.Empty;
                messenger.SendData(note.ToString(), Tokens.AppLayout);
            }
            messenger.SendData(note, Tokens.Storage);
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            visited++;
            var statistics = GetStatistics(sender.ToString());
            messenger.SendData(statistics.ToString(), Tokens.AppLayout);
        }

        private StatisticsModel GetStatistics(string algorithmName)
        {
            return new()
            {
                AlgorithmName = algorithmName,
                Elapsed = timer.Elapsed,
                Visited = visited
            };
        }

        private StatisticsModel GetStatistics(IGraphPath path, string algorithmName)
        {
            var note = GetStatistics(algorithmName);
            note.Steps = path.Count;
            note.Cost = path.Cost;
            return note;
        }
    }
}
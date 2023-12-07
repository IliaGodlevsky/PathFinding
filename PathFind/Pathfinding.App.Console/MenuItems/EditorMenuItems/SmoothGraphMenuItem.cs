using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Settings;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Smoothing;
using Pathfinding.GraphLib.Smoothing.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [HighestPriority]
    internal sealed class SmoothGraphMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IMeanCost meanAlgorithm;
        private readonly IInput<ConsoleKey> input;
        private readonly GraphsPathfindingHistory history;
        private readonly Dictionary<ConsoleKey, Action> actions;

        private Stack<IReadOnlyList<int>> SmoothHistory
            => history.GetSmoothHistory(graph.GetHashCode());

        private IGraph<Vertex> graph = Graph<Vertex>.Empty;

        public SmoothGraphMenuItem(IMeanCost meanAlgorithm, 
            IInput<ConsoleKey> input,
            GraphsPathfindingHistory history)
        {
            this.meanAlgorithm = meanAlgorithm;
            this.history = history;
            this.input = input;
            actions = new Dictionary<ConsoleKey, Action>()
            {
                { Keys.Default.SubmitSmooth, Smooth },
                { Keys.Default.UndoSmooth, Undo },
                { Keys.Default.ResetSmooth, Cancel }
            };
        }

        public bool CanBeExecuted() => graph != Graph<Vertex>.Empty;

        public void Execute()
        {
            using (Cursor.HideCursor())
            {
                var key = input.Input();
                while (key != Keys.Default.SubmitSmooth)
                {
                    actions.GetOrDefault(key)?.Invoke();
                    key = input.Input();
                }
            }
        }

        private void Smooth()
        {
            SmoothHistory.Push(graph.GetCosts());
            graph.Smooth(meanAlgorithm);
        }

        private void Undo()
        {
            if (SmoothHistory.Count > 0)
            {
                graph.Reverse().ApplyCosts(SmoothHistory.Pop().Reverse());
            }
        }

        private void Cancel()
        {
            if (SmoothHistory.Count > 0)
            {
                var initPrices = SmoothHistory.Last().Reverse();
                graph.Reverse().ApplyCosts(initPrices);
                SmoothHistory.Clear();
            }
        }

        private void OnGraphCreated(GraphMessage msg)
        {
            graph = msg.Graph;
        }

        public override string ToString()
        {
            return Languages.SmoothGraphItem;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterGraph(this, Tokens.Common, OnGraphCreated);
        }
    }
}
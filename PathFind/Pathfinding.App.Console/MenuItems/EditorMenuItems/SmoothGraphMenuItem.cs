using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Settings;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Smoothing;
using Pathfinding.GraphLib.Smoothing.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [LowPriority]
    internal sealed class SmoothGraphMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IMeanCost meanAlgorithm;
        private readonly IMessenger messenger;
        private readonly IInput<ConsoleKey> input;
        private readonly Stack<IReadOnlyList<int>> costs = new();
        private readonly Dictionary<ConsoleKey, Action> actions;

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public SmoothGraphMenuItem(IMeanCost meanAlgorithm,
            IMessenger messenger, IInput<ConsoleKey> input)
        {
            this.meanAlgorithm = meanAlgorithm;
            this.messenger = messenger;
            this.input = input;
            actions = new()
            {
                { Keys.Default.SmoothGraph, Smooth },
                { Keys.Default.UndoSmooth, Undo },
                { Keys.Default.ResetSmooth, Cancel }
            };
        }

        public bool CanBeExecuted() => graph != Graph2D<Vertex>.Empty;

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
            costs.Push(graph.GetCosts());
            graph.Smooth(meanAlgorithm);
        }

        private void Undo()
        {
            if (costs.Count > 0)
            {
                graph.Reverse().ApplyCosts(costs.Pop().Reverse());
            }
        }

        private void Cancel()
        {
            if (costs.Count > 0)
            {
                var initPrices = costs.Last().Reverse();
                graph.Reverse().ApplyCosts(initPrices);
                costs.Clear();
            }
        }

        private void OnGraphCreated(Graph2D<Vertex> graph)
        {
            this.graph = graph;
            costs.Clear();
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
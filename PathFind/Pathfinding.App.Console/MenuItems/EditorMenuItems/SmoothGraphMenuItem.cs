using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
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
                { ConsoleKey.W, Smooth },
                { ConsoleKey.S, Undo },
                { ConsoleKey.Escape, Cancel }
            };
        }

        public bool CanBeExecuted() => graph != Graph2D<Vertex>.Empty;

        public void Execute()
        {
            using (Cursor.HideCursor())
            {
                var key = input.Input();
                while (key != ConsoleKey.Enter)
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
                graph.ApplyCosts(costs.Pop());
            }
        }

        private void Cancel()
        {
            if (costs.Count > 0)
            {
                var initPrices = costs.Last();
                graph.ApplyCosts(initPrices);
                costs.Clear();               
            }
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            graph = message.Graph;
            costs.Clear();
        }

        public override string ToString()
        {
            return Languages.SmoothGraph;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }
    }
}
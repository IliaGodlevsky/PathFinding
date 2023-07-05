using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess;
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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.GraphMenuItems
{
    [HighestPriority]
    internal sealed class SmoothGraphMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IMeanCost meanAlgorithm;
        private readonly IMessenger messenger;
        private readonly IInput<ConsoleKey> input;
        private readonly GraphsPathfindingHistory history;

        private Stack<IReadOnlyList<int>> SmoothHistory => history.GetFor(graph).SmoothHistory;

        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public SmoothGraphMenuItem(IMeanCost meanAlgorithm,
            IMessenger messenger, IInput<ConsoleKey> input,
            GraphsPathfindingHistory history)
        {
            this.meanAlgorithm = meanAlgorithm;
            this.messenger = messenger;
            this.history = history;
            this.input = input;
        }

        public bool CanBeExecuted() => graph != Graph2D<Vertex>.Empty;

        public void Execute()
        {
            using (Cursor.HideCursor())
            {
                var key = input.Input();
                while (key != Keys.Default.SubmitSmooth)
                {
                    if (key == Keys.Default.SmoothGraph)
                        Smooth();
                    else if (key == Keys.Default.UndoSmooth)
                        Undo();
                    else if (key == Keys.Default.ResetSmooth)
                        Cancel();
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

        private void OnGraphCreated(Graph2D<Vertex> graph)
        {
            this.graph = graph;
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
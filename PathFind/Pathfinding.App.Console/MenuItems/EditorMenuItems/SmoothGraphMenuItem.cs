using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.DataAccess.UnitOfWorks;
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
    [LowPriority]
    internal sealed class SmoothGraphMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IMeanCost meanAlgorithm;
        private readonly IMessenger messenger;
        private readonly IInput<ConsoleKey> input;
        private readonly IUnitOfWork unitOfWork;
        private readonly Stack<IReadOnlyList<int>> costs = new();

        private GraphModel graph = new();

        public SmoothGraphMenuItem(IMeanCost meanAlgorithm,
            IMessenger messenger, IInput<ConsoleKey> input,
            IUnitOfWork unitOfWork)
        {
            this.meanAlgorithm = meanAlgorithm;
            this.messenger = messenger;
            this.input = input;
            this.unitOfWork = unitOfWork;
        }

        public bool CanBeExecuted() => graph.Graph != Graph2D<Vertex>.Empty;

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
                unitOfWork.UpdateGraph(graph);
            }
        }

        private void Smooth()
        {
            costs.Push(graph.Graph.GetCosts());
            graph.Graph.Smooth(meanAlgorithm);
        }

        private void Undo()
        {
            if (costs.Count > 0)
            {
                graph.Graph.Reverse().ApplyCosts(costs.Pop().Reverse());
            }
        }

        private void Cancel()
        {
            if (costs.Count > 0)
            {
                var initPrices = costs.Last().Reverse();
                graph.Graph.Reverse().ApplyCosts(initPrices);
                costs.Clear();
            }
        }

        private void OnGraphCreated(GraphModel graph)
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
            messenger.RegisterData<GraphModel>(this, Tokens.Common, OnGraphCreated);
        }
    }
}
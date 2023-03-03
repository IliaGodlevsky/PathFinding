using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.PathfindingHistoryMenuItems
{
    [HighPriority]
    internal sealed class ShowHistoryMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly IInput<int> input;
        private readonly Dictionary<Guid, string> pages = new();

        private bool isHistoryApplied = false;
        private Graph2D<Vertex> graph = Graph2D<Vertex>.Empty;

        public ShowHistoryMenuItem(IMessenger messenger, IInput<int> input)
        {
            this.input = input;
            this.messenger = messenger;
        }

        public bool CanBeExecuted()
        {
            return isHistoryApplied && pages.Count > 0;
        }

        public void Execute()
        {
            var menuList = pages.Values.Append(Languages.Quit).CreateMenuList(columnsNumber: 1);
            string inputMessage = string.Concat(menuList, "\n", Languages.AlgorithmChoiceMsg);
            using (RememberGraphState())
            {
                int index = GetAlgorithmIndex(inputMessage);
                while (index != pages.Count)
                {
                    var page = pages.ElementAt(index);
                    using (Cursor.UseCurrentPosition())
                    {
                        using (Cursor.HideCursor())
                        {
                            messenger.Send(new HistoryPageMessage(page.Key));
                            messenger.Send(new PathfindingStatisticsMessage(page.Value));
                        }
                    }
                    index = GetAlgorithmIndex(inputMessage);
                }
            }
        }

        private IDisposable RememberGraphState()
        {
            var costs = graph.GetCosts();
            void ReturnCosts() => graph.ApplyCosts(costs);
            return Disposable.Use(ReturnCosts, () => messenger.Send(new ClearColorsMessage()));
        }

        private void OnGraphCreated(GraphCreatedMessage msg)
        {
            graph = msg.Graph;
            pages.Clear();
        }

        private int GetAlgorithmIndex(string message)
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                return input.Input(message, pages.Count + 1, 1) - 1;
            }
        }

        private void OnAlgorithmFinished(AlgorithmFinishedMessage msg)
        {
            if (isHistoryApplied)
            {
                pages[msg.Algorithm.Id] = msg.Statistics;
            }
        }

        private void OnHistoryApplied(ApplyHistoryMessage msg)
        {
            isHistoryApplied = msg.IsApplied;
        }

        public override string ToString()
        {
            return Languages.ShowHistory;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.Register<ApplyHistoryMessage>(this, OnHistoryApplied);
            messenger.Register<AlgorithmFinishedMessage>(this, OnAlgorithmFinished);
            messenger.Register<ClearHistoryMessage>(this, _ => pages.Clear());
            messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }
    }
}

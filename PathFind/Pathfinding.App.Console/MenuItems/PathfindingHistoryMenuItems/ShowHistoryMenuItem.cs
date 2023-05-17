using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Notes;
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
        private readonly Dictionary<Guid, StatisticsNote> pages = new();

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
            var menuList = pages.Values
                .Select(note => note.ToString())
                .Append(Languages.Quit)
                .CreateMenuList(columnsNumber: 1);
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
                            messenger.SendData(page.Key, Tokens.History);
                            messenger.SendData(page.Value.ToString(), Tokens.AppLayout);
                        }
                    }
                    index = GetAlgorithmIndex(inputMessage);
                }
            }
        }

        private Disposable RememberGraphState()
        {
            var costs = graph.GetCosts();
            return Disposable.Use(() =>
            {
                using (Cursor.HideCursor())
                {
                    graph.ApplyCosts(costs);
                    messenger.Send(new ClearColorsMessage());
                }
            });
        }

        private void SetGraph(Graph2D<Vertex> graph)
        {
            this.graph = graph;
            pages.Clear();
        }

        private int GetAlgorithmIndex(string message)
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                return input.Input(message, pages.Count + 1, 1) - 1;
            }
        }

        private void SetStatistics((PathfindingProcess Process, StatisticsNote Note) value)
        {
            if (isHistoryApplied)
            {
                pages[value.Process.Id] = value.Note;
            }
        }

        private void SetIsApplied(bool isApplied)
        {
            isHistoryApplied = isApplied;
        }

        public override string ToString()
        {
            return Languages.ShowHistory;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterData<bool>(this, Tokens.History, SetIsApplied);
            messenger.RegisterAlgorithmData<StatisticsNote>(this, Tokens.History, SetStatistics);
            messenger.Register<ClearHistoryMessage>(this, _ => pages.Clear());
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }
    }
}

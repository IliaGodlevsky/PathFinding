using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;
using Shared.Primitives.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.PathfindingHistoryMenuItems
{
    [Order(2)]
    internal sealed class ShowHistoryMenuItem : IConditionedMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IInput<int> input;
        private readonly Dictionary<Guid, string> pages = new();

        private bool isHistoryApplied = false;

        private IDisplayable MenuList => pages.Values.Append(Languages.Quit).CreateMenuList(columnsNumber: 1);

        public ShowHistoryMenuItem(IMessenger messenger, IInput<int> input)
        {
            this.input = input;
            this.messenger = messenger;
            this.messenger.Register<ApplyHistoryMessage>(this, OnHistoryApplied);
            this.messenger.Register<AlgorithmFinishedMessage>(this, OnAlgorithmFinished);
            this.messenger.Register<ClearHistoryMessage>(this, _ => ClearHistory());
            this.messenger.Register<GraphCreatedMessage>(this, _ => ClearHistory());
        }

        public bool CanBeExecuted() => isHistoryApplied && pages.Count > 0;

        public void Execute()
        {
            string inputMessage = string.Concat(MenuList, "\n", Languages.AlgorithmChoiceMsg);
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

        private void ClearHistory()
        {
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
    }
}

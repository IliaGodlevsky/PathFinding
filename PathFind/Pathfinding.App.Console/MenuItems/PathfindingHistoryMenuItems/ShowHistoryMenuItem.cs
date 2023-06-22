using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess.Models;
using Pathfinding.App.Console.DataAccess.UnitOfWorks;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
using Pathfinding.GraphLib.Core.Interface.Extensions;
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

        private bool isHistoryApplied = true;
        private GraphModel graph = new();
        private AlgorithmModel algorithm;

        public ShowHistoryMenuItem(IMessenger messenger, 
            IInput<int> input)
        {
            this.input = input;
            this.messenger = messenger;
        }

        public bool CanBeExecuted() => IsHistoryApplied();

        public void Execute()
        {
            var algorithms = history.AlgorithmRepository
                .GetAll(a => a.GraphId == graph.Id)
                .ToDictionary(a => a.Id);
            var statistics = history.StatisticsRepository
                .GetAll(s => algorithms.ContainsKey(s.AlgorithmId))
                .ToArray();
            var menuList = statistics
                .Select(note => note.ToString())
                .Append(Languages.Quit)
                .CreateMenuList(columnsNumber: 1);
            string inputMessage = string.Concat(menuList, "\n", Languages.AlgorithmChoiceMsg);
            using (RememberGraphState())
            {
                int index = GetAlgorithmId(inputMessage, algorithms);
                while (index != algorithms.Count)
                {
                    var page = statistics[index];
                    using (Cursor.UseCurrentPosition())
                    {
                        using (Cursor.HideCursor())
                        {
                            messenger.SendData(page.AlgorithmId, Tokens.History);
                            messenger.SendData(page.ToString(), Tokens.AppLayout);
                        }
                    }
                    index = GetAlgorithmId(inputMessage, algorithms);
                }
            }
        }

        private void GetStatisticsList(IReadOnlyCollection<StatisticsModel> statistics)
        {

        }

        private Disposable RememberGraphState()
        {
            var costs = graph.Graph.GetCosts();
            return Disposable.Use(() =>
            {
                using (Cursor.HideCursor())
                {
                    graph.Graph.ApplyCosts(costs);
                    messenger.Send(new ClearColorsMessage());
                }
            });
        }

        private void SetGraph(GraphModel graph)
        {
            this.graph = graph;
        }

        private void SetAlgorithm(AlgorithmModel algorithm)
        {
            this.algorithm = algorithm;
        }

        private int GetAlgorithmId(string message, 
            IReadOnlyDictionary<long, AlgorithmModel> algorithms)
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                int index = input.Input(message, algorithms.Count + 1, 1) - 1;
                return index == algorithms.Count ? algorithms.Count : index;
            }
        }

        private bool IsHistoryApplied() => isHistoryApplied;

        private void SetStatistics(StatisticsModel value)
        {
            history.AddStatistics(algorithm.Id, value);
        }

        private void SetIsApplied(bool isApplied)
        {
            isHistoryApplied = isApplied;
        }

        public override string ToString()
        {
            return Languages.ShowHistory;
        }

        private void ClearStatistics(ClearHistoryMessage msg)
        {
            
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            var token = Tokens.Bind(IsHistoryApplied, Tokens.History);
            messenger.RegisterData<bool>(this, Tokens.History, SetIsApplied);
            messenger.RegisterData<StatisticsModel>(this, token, SetStatistics);
            messenger.Register<ClearHistoryMessage>(this, token, ClearStatistics);
            messenger.RegisterData<GraphModel>(this, Tokens.Common, SetGraph);
            messenger.RegisterData<AlgorithmModel>(this, Tokens.History, SetAlgorithm);
        }
    }
}

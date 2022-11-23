using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Events;
using Pathfinding.App.Console.Attributes;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Menu.Attributes;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding.Visualization.Extensions;
using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.AlgorithmLib.History;
using Pathfinding.GraphLib.Core.Interface.Extensions;

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(1)]
    [InstancePerLifetimeScope]
    internal sealed class PathfindingHistoryViewModel : SafeViewModel, IRequireAnswerInput, IRequireIntInput
    {
        private const int MenuOffset = 8;

        private readonly Graph2D<Vertex> graph;
        private readonly Dictionary<Guid, string> startedAlgorithms;
        private readonly IMessenger messenger;
        private readonly History<PathfindingHistoryVolume> history;

        private bool isHistoryApplied;

        public IInput<int> IntInput { get; set; }

        public IInput<Answer> AnswerInput { get; set; }

        private int QuitIndex => startedAlgorithms.Count;

        private IDisplayable MenuList => startedAlgorithms.Values.Append("Quit").CreateMenuList(columnsNumber: 1);

        private string InputMessage => string.Concat(MenuList, "\n", MessagesTexts.AlgorithmChoiceMsg);

        public PathfindingHistoryViewModel(ICache<Graph2D<Vertex>> graphCache, IMessenger messenger, ILog log) 
            : base(log)
        {
            this.history = new History<PathfindingHistoryVolume>();
            this.graph = graphCache.Cached;
            this.startedAlgorithms = new Dictionary<Guid, string>();
            this.messenger = messenger;
            this.messenger.Register<IHistoryMessage>(this, true, OnHistoryMessageRecieved);
        }

        public override void Dispose()
        {
            base.Dispose();
            ClearHistory();
            messenger.Unregister(this);
        }

        private void OnHistoryMessageRecieved(IHistoryMessage message)
        {
            if (IsHistoryApplied())
            {
                switch (message)
                {
                    case AlgorithmFinishedMessage msg:
                        startedAlgorithms[message.Algorithm.Id] = msg.Statistics;
                        break;
                    case PathfindingRangeChosenMessage msg:
                        history.AddPathfindingRange(msg.Algorithm.Id, msg.Range);
                        break;
                    case PathFoundMessage msg:
                        history.AddPath(msg.Algorithm.Id, msg.Path);
                        break;
                    case SubscribeOnHistoryMessage msg:
                        msg.Algorithm.VertexVisited += OnVertexVisited;
                        break;
                }
            }
        }

        [MenuItem(MenuItemsNames.ApplyHistoryRecording, 0)]
        private void ApplyHistory()
        {
            using (Cursor.ClearInputToCurrentPosition())
            {
                isHistoryApplied = AnswerInput.Input(MessagesTexts.ApplyHistoryMsg, Answer.Range);
            }
        }

        [ExecuteSafe(nameof(ExecuteSafe))]        
        [Condition(nameof(CanShowHistory), 1)]
        [Condition(nameof(IsHistoryApplied))]
        [MenuItem(MenuItemsNames.ShowHistory, 1)]
        private void ShowHistory()
        {
            string inputMessage = InputMessage;
            int index = GetAlgorithmIndex(inputMessage);
            while (index != QuitIndex)
            {
                var page = startedAlgorithms.ElementAt(index);
                using (Cursor.RestoreCurrentPosition())
                {
                    using (Cursor.HideCursor())
                    {
                        graph.RestoreVerticesVisualState();
                        history.VisualizeHistory(page.Key, graph);
                        messenger.Send(new UpdatePathfindingStatisticsMessage(page.Value));
                    }
                }
                index = GetAlgorithmIndex(inputMessage);
            }
        }

        [MenuItem(MenuItemsNames.ClearHistory, 2)]
        private void ClearHistory()
        {
            history.Clear();
            startedAlgorithms.Clear();
        }

        private void OnVertexVisited(object sender, PathfindingEventArgs e)
        {
            if (sender is IHistoryPageKey key)
            {
                history.AddVisited(key.Id, e.Current);
            }
        }

        private int GetAlgorithmIndex(string message)
        {
            using (Cursor.ClearInputToCurrentPosition())
            {
                return IntInput.Input(message, startedAlgorithms.Count + 1, 1) - 1;
            }
        }

        [FailMessage(MessagesTexts.NoAlgorithmsWereStartedMsg)]
        private bool CanShowHistory() => startedAlgorithms.Count > 0;

        [FailMessage(MessagesTexts.HistoryWasNotApplied)]
        private bool IsHistoryApplied() => isHistoryApplied;
    }
}
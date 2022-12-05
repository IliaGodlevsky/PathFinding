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

namespace Pathfinding.App.Console.ViewModel
{
    [MenuColumnsNumber(1)]
    [InstancePerLifetimeScope]
    internal sealed class PathfindingHistoryViewModel : SafeViewModel, IRequireAnswerInput, IRequireIntInput
    {
        private readonly Dictionary<Guid, string> startedAlgorithms;
        private readonly IMessenger messenger;
        private readonly History<PathfindingHistoryVolume> history;

        private bool isHistoryApplied;

        public IInput<int> IntInput { get; set; }

        public IInput<Answer> AnswerInput { get; set; }

        private IDisplayable MenuList => startedAlgorithms.Values.Append("Quit").CreateMenuList(columnsNumber: 1);

        private Graph2D<Vertex> Graph { get; set; } = Graph2D<Vertex>.Empty;

        public PathfindingHistoryViewModel(IMessenger messenger, ILog log)
            : base(log)
        {
            this.history = new History<PathfindingHistoryVolume>();
            this.startedAlgorithms = new Dictionary<Guid, string>();
            this.messenger = messenger;
            this.messenger.Register<AlgorithmFinishedMessage>(this, OnAlgorithmFinished);
            this.messenger.Register<PathfindingRangeChosenMessage>(this, OnRangeChosen);
            this.messenger.Register<PathFoundMessage>(this, OnPathFound);
            this.messenger.Register<SubscribeOnHistoryMessage>(this, OnSubscribeOnHistory);
            this.messenger.Register<GraphCreatedMessage>(this, OnGraphCreated);
        }

        private void OnGraphCreated(GraphCreatedMessage message)
        {
            Graph = message.Graph;
        }

        public override void Dispose()
        {
            base.Dispose();
            ClearHistory();
            messenger.Unregister(this);
        }

        [MenuItem(MenuItemsNames.ApplyHistoryRecording, 0)]
        private void ApplyHistory()
        {
            using (Cursor.CleanUpAfter())
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
            string inputMessage = string.Concat(MenuList, "\n", MessagesTexts.AlgorithmChoiceMsg);
            int index = GetAlgorithmIndex(inputMessage);
            while (index != startedAlgorithms.Count)
            {
                var page = startedAlgorithms.ElementAt(index);
                using (Cursor.UseCurrentPosition())
                {
                    using (Cursor.HideCursor())
                    {
                        Graph.RestoreVerticesVisualState();
                        history.VisualizeHistory(page.Key, Graph);
                        messenger.Send(new PathfindingStatisticsMessage(page.Value));
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
            using (Cursor.CleanUpAfter())
            {
                return IntInput.Input(message, startedAlgorithms.Count + 1, 1) - 1;
            }
        }

        [FailMessage(MessagesTexts.NoAlgorithmsWereStartedMsg)]
        private bool CanShowHistory() => startedAlgorithms.Count > 0;

        [FailMessage(MessagesTexts.HistoryWasNotApplied)]
        private bool IsHistoryApplied() => isHistoryApplied;

        private void OnMessageRecieved(Action action) 
        {
            if (IsHistoryApplied())
            {
                action();
            }
        }

        private void OnAlgorithmFinished(AlgorithmFinishedMessage msg)
        {
            OnMessageRecieved(() => startedAlgorithms[msg.Algorithm.Id] = msg.Statistics);
        }

        private void OnRangeChosen(PathfindingRangeChosenMessage msg)
        {
            OnMessageRecieved(() => history.AddPathfindingRange(msg.Algorithm.Id, msg.Range));
        }

        private void OnPathFound(PathFoundMessage msg)
        {
            OnMessageRecieved(() => history.AddPath(msg.Algorithm.Id, msg.Path));
        }

        private void OnSubscribeOnHistory(SubscribeOnHistoryMessage msg)
        {
            OnMessageRecieved(() => msg.Algorithm.VertexVisited += OnVertexVisited);
        }
    }
}
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
        private const int MenuOffset = 6;

        private readonly Graph2D<Vertex> graph;
        private readonly Dictionary<Guid, string> startedAlgorithms;
        private readonly IMessenger messenger;
        private readonly History<PathfindingHistoryVolume> history;

        private bool isHistoryApplied;

        public IInput<int> IntInput { get; set; }

        public IInput<Answer> AnswerInput { get; set; }

        private int QuitIndex => startedAlgorithms.Count;

        private int Offset => startedAlgorithms.Count + MenuOffset;

        private IDisplayable MenuList => startedAlgorithms.Values.Append("Quit").CreateMenuList(columnsNumber: 1);

        private string InputMessage => MenuList + "\n" + MessagesTexts.AlgorithmChoiceMsg;

        public PathfindingHistoryViewModel(ICache<Graph2D<Vertex>> graphCache, IMessenger messenger, ILog log) 
            : base(log)
        {
            this.history = new History<PathfindingHistoryVolume>();
            this.graph = graphCache.Cached;
            this.startedAlgorithms = new Dictionary<Guid, string>();
            this.messenger = messenger;            
        }

        private void OnHistoryMessageRecieved(IHistoryMessage message)
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
                    startedAlgorithms[message.Algorithm.Id] = string.Empty;
                    history.AddObstacles(msg.Algorithm.Id, graph.GetObstaclesCoordinates());
                    msg.Algorithm.VertexVisited += OnVertexVisited;
                    break;
            }
        }

        [MenuItem(MenuItemsNames.ApplyHistoryRecording, 0)]
        private void ApplyHistory()
        {
            isHistoryApplied = AnswerInput.Input(MessagesTexts.ApplyHistoryMsg, Answer.Range);
            if (isHistoryApplied)
            {
                messenger.Register<IHistoryMessage>(this, true, OnHistoryMessageRecieved);
            }
            else
            {
                messenger.Unregister<IHistoryMessage>(this, OnHistoryMessageRecieved);
            }
        }

        [ExecuteSafe(nameof(ExecuteSafe))]
        [Condition(nameof(IsHistoryApplied))]
        [Condition(nameof(CanShowHistory), 1)]
        [MenuItem(MenuItemsNames.ShowHistory, 1)]
        private void ShowHistory()
        {
            string inputMessage = InputMessage;
            int index = GetAlgorithmIndex(inputMessage);
            while (index != QuitIndex)
            {
                var id = startedAlgorithms.Keys.ElementAt(index);
                using (Cursor.HideCursor())
                {
                    graph.RestoreVerticesVisualState();
                    history.VisualizeHistory(id, graph);
                }
                Screen.SetCursorPositionUnderMenu(Offset);
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
            return IntInput.Input(message, startedAlgorithms.Count + 1, 1) - 1;
        }

        [FailMessage(MessagesTexts.NoAlgorithmsWereStartedMsg)]
        private bool CanShowHistory() => startedAlgorithms.Count > 0;

        [FailMessage(MessagesTexts.HistoryWasNotApplied)]
        private bool IsHistoryApplied() => isHistoryApplied;
    }
}
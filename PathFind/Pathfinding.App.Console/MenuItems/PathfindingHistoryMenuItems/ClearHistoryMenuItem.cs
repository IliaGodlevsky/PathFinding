using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.DataAccess;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;

namespace Pathfinding.App.Console.MenuItems.PathfindingHistoryMenuItems
{
    [MediumPriority]
    internal sealed class ClearHistoryMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly GraphsPathfindingHistory history;

        private bool isHistoryApplied = true;
        private IGraph<Vertex> graph = Graph<Vertex>.Empty;

        public ClearHistoryMenuItem(IMessenger messenger,
            GraphsPathfindingHistory history)
        {
            this.history = history;
            this.messenger = messenger;
        }

        public bool CanBeExecuted()
        {
            return isHistoryApplied
                && history.GetFor(graph).Algorithms.Count > 0;
        }

        public void Execute()
        {
            messenger.SendData(new ClearHistoryMessage(), Tokens.History);
        }

        private void SetIsApplied(bool isApplied)
        {
            isHistoryApplied = isApplied;
        }

        public override string ToString()
        {
            return Languages.ClearHistory;
        }

        public void RegisterHanlders(IMessenger messenger)
        {
            messenger.RegisterData<bool>(this, Tokens.History, SetIsApplied);
            messenger.RegisterGraph(this, Tokens.Common, SetGraph);
        }

        private void SetGraph(IGraph<Vertex> graph)
        {
            this.graph = graph;
        }
    }
}

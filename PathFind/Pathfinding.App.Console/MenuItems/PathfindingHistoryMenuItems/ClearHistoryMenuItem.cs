using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;

namespace Pathfinding.App.Console.MenuItems.PathfindingHistoryMenuItems
{
    [MediumPriority]
    internal sealed class ClearHistoryMenuItem : IConditionedMenuItem
    {
        private readonly IMessenger messenger;

        private bool isHistoryApplied;

        public ClearHistoryMenuItem(IMessenger messenger)
        {
            this.messenger = messenger;
            this.messenger.Register<ApplyHistoryMessage>(this, OnHistoryApplied);
        }

        public bool CanBeExecuted() => isHistoryApplied;

        public void Execute()
        {
            messenger.Send(new ClearHistoryMessage());
        }

        private void OnHistoryApplied(ApplyHistoryMessage message)
        {
            isHistoryApplied = message.IsApplied;
        }

        public override string ToString()
        {
            return Languages.ClearHistory;
        }
    }
}

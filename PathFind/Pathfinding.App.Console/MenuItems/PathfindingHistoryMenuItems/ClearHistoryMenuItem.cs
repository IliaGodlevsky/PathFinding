using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;

namespace Pathfinding.App.Console.MenuItems.PathfindingHistoryMenuItems
{
    [MediumPriority]
    internal sealed class ClearHistoryMenuItem : IConditionedMenuItem, ICanRecieveMessage
    {
        private readonly IMessenger messenger;

        private bool isHistoryApplied;

        public ClearHistoryMenuItem(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public bool CanBeExecuted() => isHistoryApplied;

        public void Execute()
        {
            messenger.Send(new ClearHistoryMessage());
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
        }
    }
}

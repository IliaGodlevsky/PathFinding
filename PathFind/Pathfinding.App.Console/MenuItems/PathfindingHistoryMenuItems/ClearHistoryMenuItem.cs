using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;
using Shared.Primitives.Attributes;

namespace Pathfinding.App.Console.MenuItems.PathfindingHistoryMenuItems
{
    [Order(3)]
    internal sealed class ClearHistoryMenuItem : IMenuItem
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

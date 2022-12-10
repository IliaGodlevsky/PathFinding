using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;

namespace Pathfinding.App.Console.MenuItems.PathfindingHistoryMenuItems
{
    internal sealed class ClearHistoryMenuItem : IMenuItem
    {
        private readonly IMessenger messenger;

        private bool isHistoryApplied;

        public ClearHistoryMenuItem(IMessenger messenger)
        {
            this.messenger = messenger;
            this.messenger.Register<ApplyHistoryMessage>(this, OnHistoryApplied);
        }

        public int Order => 3;

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
            return "Clear hsitory";
        }
    }
}

using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.MenuItems.PathfindingHistoryMenuItems
{
    [HighestPriority]
    internal sealed class ApplyHistoryMenuItem : IMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IInput<Answer> answerInput;

        public ApplyHistoryMenuItem(IMessenger messenger, IInput<Answer> answerInput)
        {
            this.messenger = messenger;
            this.answerInput = answerInput;
        }

        public void Execute()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                string message = MessagesTexts.ApplyHistoryMsg;
                bool isApplied = answerInput.Input(message, Answer.Range);
                messenger.Send(new ApplyHistoryMessage(isApplied));
            }
        }

        public override string ToString()
        {
            return Languages.ApplyHistory;
        }
    }
}

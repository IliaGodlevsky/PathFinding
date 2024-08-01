using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task ExecuteAsync(CancellationToken token = default)
        {
            if (token.IsCancellationRequested) return;
            using (Cursor.UseCurrentPositionWithClean())
            {
                string message = MessagesTexts.ApplyHistoryMsg;
                bool isApplied = answerInput.Input(message, Answer.Range);
                var isAppliedMsg = new IsAppliedMessage(isApplied);
                messenger.Send(isAppliedMsg, Tokens.History);
                await Task.CompletedTask;
            }
        }

        public override string ToString()
        {
            return Languages.ApplyHistory;
        }
    }
}

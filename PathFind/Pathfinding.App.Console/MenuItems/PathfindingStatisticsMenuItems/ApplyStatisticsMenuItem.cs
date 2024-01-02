using CommunityToolkit.Mvvm.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Messaging;
using Pathfinding.App.Console.Messaging.Messages;
using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.MenuItems.PathfindingStatisticsMenuItems
{
    [HighestPriority]
    internal sealed class ApplyStatisticsMenuItem : IMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IInput<Answer> answerInput;

        public ApplyStatisticsMenuItem(IMessenger messenger, IInput<Answer> answerInput)
        {
            this.messenger = messenger;
            this.answerInput = answerInput;
        }

        public void Execute()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                string message = MessagesTexts.ApplyStatisticsMsg;
                bool isApplied = answerInput.Input(message, Answer.Range);
                var msg = new IsAppliedMessage(isApplied);
                messenger.Send(msg, Tokens.Statistics);
            }
        }

        public override string ToString()
        {
            return Languages.ApplyStatistics;
        }
    }
}

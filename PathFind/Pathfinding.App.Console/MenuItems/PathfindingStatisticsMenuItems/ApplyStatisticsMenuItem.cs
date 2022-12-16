using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.MenuItems.PathfindingStatisticsMenuItems
{
    internal sealed class ApplyStatisticsMenuItem : IMenuItem
    {
        private readonly IMessenger messenger;
        private readonly IInput<Answer> answerInput;

        public int Order => 1;

        public ApplyStatisticsMenuItem(IMessenger messenger, IInput<Answer> answerInput) 
        {
            this.messenger = messenger;
            this.answerInput = answerInput;
        }

        public bool CanBeExecuted() => true;

        public void Execute()
        {
            using (Cursor.CleanUpAfter())
            {
                string message = MessagesTexts.ApplyStatisticsMsg;
                bool isApplied = answerInput.Input(message, Answer.Range);
                messenger.Send(new ApplyStatisticsMessage(isApplied));
            }
        }

        public override string ToString()
        {
            return Languages.ApplyStatistics;
        }
    }
}

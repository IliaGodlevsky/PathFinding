using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.MenuItems.PathfindingHistoryMenuItems
{
    internal sealed class ApplyHistoryMenuItem : IMenuItem
    {
        private static readonly string Message = MessagesTexts.ApplyHistoryMsg;

        private readonly IMessenger messenger;
        private readonly IInput<Answer> answerInput;

        public ApplyHistoryMenuItem(IMessenger messenger, IInput<Answer> answerInput)
        {
            this.messenger = messenger;
            this.answerInput = answerInput;
        }

        public int Order => 1;

        public bool CanBeExecuted()
        {
            return true;
        }

        public void Execute()
        {
            using (Cursor.CleanUpAfter())
            {
                bool isApplied = answerInput.Input(Message, Answer.Range);
                messenger.Send(new ApplyHistoryMessage(isApplied));
            }
        }

        public override string ToString()
        {
            return "Apply history";
        }
    }
}

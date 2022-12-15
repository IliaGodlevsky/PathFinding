using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.MenuItems.PathfindingVisualizationMenuItems
{
    internal sealed class ApplyVisualizationMenuItem : IMenuItem
    {
        private static readonly string Message = MessagesTexts.ApplyVisualizationMsg;

        private readonly IInput<Answer> answerInput;
        private readonly IMessenger messenger;

        public ApplyVisualizationMenuItem(IInput<Answer> answerInput, IMessenger messenger)
        {
            this.answerInput = answerInput;
            this.messenger = messenger;
        }

        public int Order => 1;

        public void Execute()
        {
            using (Cursor.CleanUpAfter())
            {
                bool isApplied = answerInput.Input(Message, Answer.Range);
                messenger.Send(new ApplyVisualizationMessage(isApplied));
            }
        }

        public bool CanBeExecuted() => true;

        public override string ToString()
        {
            return Languages.ApplyVisualization;
        }
    }
}

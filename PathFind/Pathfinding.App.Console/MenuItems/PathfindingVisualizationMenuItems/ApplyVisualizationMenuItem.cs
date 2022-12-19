using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;
using Pathfinding.App.Console.Model;
using Shared.Primitives.Attributes;

namespace Pathfinding.App.Console.MenuItems.PathfindingVisualizationMenuItems
{
    [Order(1)]
    internal sealed class ApplyVisualizationMenuItem : IMenuItem
    {
        private readonly IInput<Answer> answerInput;
        private readonly IMessenger messenger;

        public ApplyVisualizationMenuItem(IInput<Answer> answerInput, IMessenger messenger)
        {
            this.answerInput = answerInput;
            this.messenger = messenger;
        }

        public void Execute()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                string message = MessagesTexts.ApplyVisualizationMsg;
                bool isApplied = answerInput.Input(message, Answer.Range);
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

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

namespace Pathfinding.App.Console.MenuItems.PathfindingVisualizationMenuItems
{
    [HighestPriority]
    internal sealed class ApplyVisualizationMenuItem : IMenuItem
    {
        private readonly IInput<Answer> answerInput;
        private readonly IMessenger messenger;

        public ApplyVisualizationMenuItem(IInput<Answer> answerInput, IMessenger messenger)
        {
            this.answerInput = answerInput;
            this.messenger = messenger;
        }

        public async Task ExecuteAsync(CancellationToken token = default)
        {
            if (token.IsCancellationRequested) return;
            using (Cursor.UseCurrentPositionWithClean())
            {
                string message = MessagesTexts.ApplyVisualizationMsg;
                bool isApplied = answerInput.Input(message, Answer.Range);
                var msg = new IsAppliedMessage(isApplied);
                messenger.Send(msg, Tokens.Visualization);
                await Task.CompletedTask;
            }
        }

        public override string ToString()
        {
            return Languages.ApplyVisualization;
        }
    }
}

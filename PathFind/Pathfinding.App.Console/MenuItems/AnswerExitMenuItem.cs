using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.MenuItems
{
    [LowestPriority]
    internal sealed class AnswerExitMenuItem(IInput<Answer> input) : ExitMenuItem
    {
        private readonly IInput<Answer> input = input;

        public override async Task ExecuteAsync(CancellationToken token = default)
        {
            if (token.IsCancellationRequested) return;
            using (Cursor.UseCurrentPositionWithClean())
            {
                bool isExit = input.Input(MessagesTexts.ExitAppMsg, Answer.Range);
                if (isExit)
                {
                    await base.ExecuteAsync(token);
                }
            }
        }
    }
}

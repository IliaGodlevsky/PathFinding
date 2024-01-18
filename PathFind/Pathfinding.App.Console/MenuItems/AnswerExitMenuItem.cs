using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.MenuItems
{
    [LowestPriority]
    internal sealed class AnswerExitMenuItem(IInput<Answer> input) : ExitMenuItem
    {
        private readonly IInput<Answer> input = input;

        public override void Execute()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                bool isExit = input.Input(MessagesTexts.ExitAppMsg, Answer.Range);
                if (isExit)
                {
                    base.Execute();
                }
            }
        }
    }
}

using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.MenuItems
{
    internal sealed class AnswerExitMenuItem : ExitMenuItem
    {
        private readonly IInput<Answer> input;

        public AnswerExitMenuItem(IInput<Answer> input)
        {
            this.input = input;
        }

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

using Pathfinding.App.Console.Exceptions;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.MenuItems
{
    internal sealed class AnswerExitMenuItem : IMenuItem
    {
        private readonly IInput<Answer> input;

        public int Order => int.MaxValue;

        public AnswerExitMenuItem(IInput<Answer> input)
        {
            this.input = input;
        }

        bool IMenuItem.CanBeExecuted() => true;

        public void Execute()
        {
            using (Cursor.CleanUpAfter())
            {
                bool isExit = input.Input(MessagesTexts.ExitAppMsg, Answer.Range);
                if (isExit)
                {
                    throw new ExitRequiredException();
                }
            }
        }

        public override string ToString()
        {
            return Languages.Exit;
        }
    }
}

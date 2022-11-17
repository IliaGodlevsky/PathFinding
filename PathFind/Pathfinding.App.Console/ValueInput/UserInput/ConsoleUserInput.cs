using Pathfinding.App.Console.Interface;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.ValueInput.UserInput
{
    internal abstract class ConsoleUserInput<TOutput, TInner> : IInput<TOutput>
    {
        public TOutput Input()
        {
            TInner result;
            string userInput = ColorfulConsole.ReadLine();
            while (!IsValidInput(userInput, out result))
            {
                ColorfulConsole.Write(MessagesTexts.BadInputMsg);
                userInput = ColorfulConsole.ReadLine();
            }
            return Convert(result);
        }

        protected abstract TOutput Convert(TInner inner);

        protected abstract bool IsValidInput(string input, out TInner parsed);
    }
}

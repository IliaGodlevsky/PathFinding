using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;

namespace Pathfinding.App.Console.ValueInput.UserInput
{
    internal abstract class ConsoleUserInput<TOutput, TInner> : IInput<TOutput>
    {
        public TOutput Input()
        {
            TInner result;
            string userInput = Terminal.ReadLine();
            while (!TryParse(userInput, out result))
            {
                Terminal.Write(Languages.BadInputMsg);
                userInput = Terminal.ReadLine();
            }
            return Interpret(result);
        }

        protected abstract TOutput Interpret(TInner inner);

        protected abstract bool TryParse(string input, out TInner parsed);
    }
}

using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;

namespace Pathfinding.App.Console.ValueInput.UserInput
{
    internal abstract class ConsoleUserInput<TOutput, TInner> : IInput<TOutput>
    {
        public TOutput Input()
        {
            TInner result;
            string userInput = System.Console.ReadLine();
            while (!IsValidInput(userInput, out result))
            {
                System.Console.Write(Languages.BadInputMsg);
                userInput = System.Console.ReadLine();
            }
            return Convert(result);
        }

        protected abstract TOutput Convert(TInner inner);

        protected abstract bool IsValidInput(string input, out TInner parsed);
    }
}

using Pathfinding.App.Console.Interface;


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
                System.Console.Write(MessagesTexts.BadInputMsg);
                userInput = System.Console.ReadLine();
            }
            return Convert(result);
        }

        protected abstract TOutput Convert(TInner inner);

        protected abstract bool IsValidInput(string input, out TInner parsed);
    }
}

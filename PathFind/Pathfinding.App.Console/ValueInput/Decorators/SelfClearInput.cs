using Pathfinding.App.Console.Interface;

namespace Pathfinding.App.Console.ValueInput.Decorators
{
    internal sealed class SelfClearInput<T> : IInput<T>
    {
        private readonly IInput<T> input;

        public SelfClearInput(IInput<T> input)
        {
            this.input = input;
        }

        public T Input()
        {
            using (Cursor.UsePositionAndClear())
            {
                return input.Input();
            }
        }
    }
}

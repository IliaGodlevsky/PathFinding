using Pathfinding.App.Console.EventHandlers;
using Pathfinding.App.Console.ValueInput.UserInput;
using Shared.Process.EventHandlers;
using Shared.Process.Interface;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ConsoleKeystrokesHook : IInterruptable, IProcess
    {
        public event ConsoleKeyPressedEventHandler KeyPressed;
        public event ProcessEventHandler Interrupted;
        public event ProcessEventHandler Started;
        public event ProcessEventHandler Finished;

        private readonly ConsoleUserKeyInput input = new();

        public bool IsInProcess { get; private set; }

        public void Interrupt()
        {
            IsInProcess = false;
            Finished?.Invoke(this, new());
            Interrupted?.Invoke(this, new());
        }

        public void Start()
        {
            Started?.Invoke(this, new());
            IsInProcess = true;
            while (IsInProcess)
            {
                var key = input.Input();
                KeyPressed?.Invoke(this, new(key));
            }
        }
    }
}
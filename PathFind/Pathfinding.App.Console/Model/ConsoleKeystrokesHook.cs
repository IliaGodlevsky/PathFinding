using Pathfinding.App.Console.EventArguments;
using Pathfinding.App.Console.EventHandlers;
using Pathfinding.App.Console.ValueInput.UserInput;
using Shared.Process.EventArguments;
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
            Finished?.Invoke(this, new ProcessEventArgs());
            Interrupted?.Invoke(this, new ProcessEventArgs());
        }

        public void Start()
        {
            Started?.Invoke(this, new ProcessEventArgs());
            IsInProcess = true;
            while (IsInProcess)
            {
                var key = input.Input();
                KeyPressed?.Invoke(this, new ConsoleKeyPressedEventArgs(key));
            }
        }
    }
}
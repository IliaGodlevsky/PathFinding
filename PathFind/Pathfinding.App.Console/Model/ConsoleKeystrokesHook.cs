using Pathfinding.App.Console.EventArguments;
using Pathfinding.App.Console.EventHandlers;
using Pathfinding.App.Console.Interface;
using Shared.Process.EventArguments;
using Shared.Process.EventHandlers;
using Shared.Process.Interface;
using System;

namespace Pathfinding.App.Console.Model
{
    internal sealed class ConsoleKeystrokesHook : IInterruptable, IProcess, IRequireConsoleKeyInput
    {
        public event ConsoleKeyPressedEventHandler KeyPressed;
        public event ProcessEventHandler Interrupted;
        public event ProcessEventHandler Started;
        public event ProcessEventHandler Finished;

        public bool IsInProcess { get; private set; }

        public IInput<ConsoleKey> KeyInput { get; set; }

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
                var key = KeyInput.Input();
                KeyPressed?.Invoke(this, new ConsoleKeyPressedEventArgs(key));
            }
        }
    }
}
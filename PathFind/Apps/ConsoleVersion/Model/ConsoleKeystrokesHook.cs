using ConsoleVersion.EventArguments;
using ConsoleVersion.EventHandlers;
using System;

namespace ConsoleVersion.Model
{
    internal sealed class ConsoleKeystrokesHook
    {
        public event ConsoleKeyPressedEventHandler KeyPressed;

        public static ConsoleKeystrokesHook Instance => instance.Value;

        private bool IsHookingRequired { get; set; }

        public void CancelHookingConsoleKeystrokes()
        {
            IsHookingRequired = false;
        }

        public void StartHookingConsoleKeystrokes()
        {
            IsHookingRequired = true;
            while (IsHookingRequired)
            {
                var key = Console.ReadKey(true).Key;
                var args = new ConsoleKeyPressedEventArgs(key);
                KeyPressed?.Invoke(this, args);
            }
        }

        private static readonly Lazy<ConsoleKeystrokesHook> instance 
            = new Lazy<ConsoleKeystrokesHook>(() => new ConsoleKeystrokesHook());
    }
}
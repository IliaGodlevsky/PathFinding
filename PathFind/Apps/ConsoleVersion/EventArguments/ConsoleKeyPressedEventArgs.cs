using System;

namespace ConsoleVersion.EventArguments
{
    internal class ConsoleKeyPressedEventArgs : EventArgs
    {
        public ConsoleKey PressedKey { get; }

        public ConsoleKeyPressedEventArgs(ConsoleKey pressedKey)
        {
            PressedKey = pressedKey;
        }
    }
}

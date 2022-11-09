using System;

namespace Pathfinding.App.Console.EventArguments
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

using ConsoleVersion.Interface;
using System;

namespace ConsoleVersion.ValueInput.UserInput
{
    internal sealed class ConsoleUserKeyInput : IInput<ConsoleKey>
    {
        public ConsoleKey Input() => Console.ReadKey(true).Key;
    }
}

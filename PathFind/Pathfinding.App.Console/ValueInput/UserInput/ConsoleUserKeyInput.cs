using Pathfinding.App.Console.Interface;
using System;

namespace Pathfinding.App.Console.ValueInput.UserInput
{
    internal sealed class ConsoleUserKeyInput : IInput<ConsoleKey>
    {
        public ConsoleKey Input() => System.Console.ReadKey(true).Key;
    }
}

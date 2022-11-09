using Pathfinding.App.Console.Interface;
using System;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.ValueInput.UserInput
{
    internal sealed class ConsoleUserKeyInput : IInput<ConsoleKey>
    {
        public ConsoleKey Input() => ColorfulConsole.ReadKey(true).Key;
    }
}

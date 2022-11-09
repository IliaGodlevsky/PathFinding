using Pathfinding.App.Console.Interface;
using System;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.ValueInput.UserInput
{
    internal sealed class ConsoleUserStringInput : IInput<string>
    {
        public string Input() => ColorfulConsole.ReadLine();
    }
}

using Pathfinding.App.Console.Interface;
using System;

namespace Pathfinding.App.Console.ValueInput.UserInput
{
    internal sealed class ConsoleUserStringInput : IInput<string>
    {
        public string Input() => System.Console.ReadLine();
    }
}

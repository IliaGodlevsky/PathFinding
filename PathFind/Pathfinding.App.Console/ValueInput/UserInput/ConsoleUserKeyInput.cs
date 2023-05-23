using Pathfinding.App.Console.Interface;
using System;

namespace Pathfinding.App.Console.ValueInput.UserInput
{
    internal sealed class ConsoleUserKeyInput : IInput<ConsoleKey>
    {
        private readonly bool intercept;

        public ConsoleUserKeyInput(bool intercept = true) 
        { 
            this.intercept = intercept;
        }

        public ConsoleKey Input() => System.Console.ReadKey(intercept).Key;
    }
}

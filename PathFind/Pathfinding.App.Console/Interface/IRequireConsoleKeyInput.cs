using System;

namespace Pathfinding.App.Console.Interface
{
    internal interface IRequireConsoleKeyInput
    {
        IInput<ConsoleKey> KeyInput { get; set; }
    }
}

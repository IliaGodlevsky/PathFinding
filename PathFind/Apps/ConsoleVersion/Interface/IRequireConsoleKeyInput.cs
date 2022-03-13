using System;

namespace ConsoleVersion.Interface
{
    internal interface IRequireConsoleKeyInput
    {
        IInput<ConsoleKey> KeyInput { get; set; }
    }
}

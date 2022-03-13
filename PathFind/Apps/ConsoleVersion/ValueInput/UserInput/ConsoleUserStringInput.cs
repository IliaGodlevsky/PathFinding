using ConsoleVersion.Interface;
using System;

namespace ConsoleVersion.ValueInput.UserInput
{
    internal sealed class ConsoleUserStringInput : IInput<string>
    {
        public string Input() => Console.ReadLine();
    }
}

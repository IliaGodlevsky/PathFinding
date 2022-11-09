using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.ValueInput.RecordingInput;
using System;

namespace ConsoleVersion.ValueInput.RecordingInput
{
    internal sealed class RecordingConsoleKeyInput : RecordingInput<ConsoleKey>
    {
        public RecordingConsoleKeyInput(IInput<ConsoleKey> input)
           : base(input, "Script_ConsoleKey.txt")
        {

        }
    }
}
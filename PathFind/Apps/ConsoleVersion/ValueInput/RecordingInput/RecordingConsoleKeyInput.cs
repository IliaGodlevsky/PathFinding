using ConsoleVersion.Interface;
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
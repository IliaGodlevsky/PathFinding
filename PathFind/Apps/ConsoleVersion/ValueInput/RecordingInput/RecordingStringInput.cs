using ConsoleVersion.Interface;

namespace ConsoleVersion.ValueInput.RecordingInput
{
    internal sealed class RecordingStringInput : RecordingInput<string>
    {
        public RecordingStringInput(IInput<string> input)
            : base(input, "Script_string.txt")
        {

        }
    }
}
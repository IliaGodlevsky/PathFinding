using ConsoleVersion.Interface;

namespace ConsoleVersion.ValueInput.RecordingInput
{
    internal sealed class RecordingIntInput : RecordingInput<int>
    {
        public RecordingIntInput(IInput<int> input) 
            : base(input, "Script_int.txt")
        {

        }
    }
}

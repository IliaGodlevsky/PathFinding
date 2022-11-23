using Pathfinding.App.Console.Interface;

namespace Pathfinding.App.Console.ValueInput.RecordingInput
{
    internal sealed class RecordingStringInput : RecordingInput<string>
    {
        public RecordingStringInput(IInput<string> input)
            : base(input, "Script_String.txt")
        {

        }
    }
}
using Pathfinding.App.Console.Interface;

namespace Pathfinding.App.Console.ValueInput.RecordingInput
{
    internal sealed class RecordingIntInput : RecordingInput<int>
    {
        public RecordingIntInput(IInput<int> input)
            : base(input, "Script_int.txt")
        {

        }
    }
}

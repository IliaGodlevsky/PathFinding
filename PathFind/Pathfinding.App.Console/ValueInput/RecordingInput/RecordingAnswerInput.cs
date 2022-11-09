using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.ValueInput.RecordingInput
{
    internal sealed class RecordingAnswerInput : RecordingInput<Answer>
    {
        public RecordingAnswerInput(IInput<Answer> input)
            : base(input, "Script_Answer.txt")
        {
        }
    }
}

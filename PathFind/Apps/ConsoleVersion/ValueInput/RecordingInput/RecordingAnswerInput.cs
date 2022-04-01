using ConsoleVersion.Enums;
using ConsoleVersion.Interface;

namespace ConsoleVersion.ValueInput.RecordingInput
{
    internal sealed class RecordingAnswerInput : RecordingInput<Answer>
    {
        public RecordingAnswerInput(IInput<Answer> input)
            : base(input, "Script_Answer.txt")
        {
        }
    }
}

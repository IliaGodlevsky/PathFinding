using ConsoleVersion.Interface;
using ConsoleVersion.Model;

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

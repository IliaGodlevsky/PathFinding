using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using System.IO;

namespace Pathfinding.App.Console.ValueInput.RecordingInput
{
    internal sealed class RecordingAnswerInput : RecordingInput<Answer>
    {
        public RecordingAnswerInput(IInput<Answer> input)
            : base(input, "Script_Answer.txt")
        {
        }

        protected override void Write(StreamWriter writer, Answer value)
        {
            writer.WriteLine("Record:{0}", (int)value);
        }
    }
}

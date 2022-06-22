using ConsoleVersion.Model;

namespace ConsoleVersion.ValueInput.ProgrammedInput.FromFile
{
    internal sealed class FromFileProgrammedAnswerInput : FromFileProgrammedInput<Answer>
    {
        public FromFileProgrammedAnswerInput() : base("Script_Answer.txt")
        {
        }

        protected override bool Parse(string value, out Answer output)
        {
            return Answer.TryParse(value, out output);
        }
    }
}
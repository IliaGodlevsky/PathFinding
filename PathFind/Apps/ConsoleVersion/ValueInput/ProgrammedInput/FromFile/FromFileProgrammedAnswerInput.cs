using ConsoleVersion.Enums;
using System;

namespace ConsoleVersion.ValueInput.ProgrammedInput.FromFile
{
    internal sealed class FromFileProgrammedAnswerInput : FromFileProgrammedInput<Answer>
    {
        public FromFileProgrammedAnswerInput() : base("Script_Answer.txt")
        {
        }

        protected override bool Parse(string value, out Answer output)
        {
            return Enum.TryParse(value, ignoreCase: true, out output);
        }
    }
}
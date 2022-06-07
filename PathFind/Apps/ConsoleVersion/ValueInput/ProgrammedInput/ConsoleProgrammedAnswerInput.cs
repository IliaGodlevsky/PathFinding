using ConsoleVersion.Enums;

namespace ConsoleVersion.ValueInput.ProgrammedInput
{
    internal sealed class ConsoleProgrammedAnswerInput : ConsoleProgrammedInput<Answer>
    {
        private const Answer ExitApplication = Answer.Yes;
    }
}

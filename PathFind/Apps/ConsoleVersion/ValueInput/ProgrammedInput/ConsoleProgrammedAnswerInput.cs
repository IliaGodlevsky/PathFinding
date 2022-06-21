using ConsoleVersion.Model;

namespace ConsoleVersion.ValueInput.ProgrammedInput
{
    internal sealed class ConsoleProgrammedAnswerInput : ConsoleProgrammedInput<Answer>
    {
        private static readonly Answer ExitApplication = Answer.Yes;
    }
}

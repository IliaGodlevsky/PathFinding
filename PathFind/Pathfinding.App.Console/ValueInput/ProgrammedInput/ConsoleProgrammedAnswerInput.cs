using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.ValueInput.ProgrammedInput
{
    internal sealed class ConsoleProgrammedAnswerInput : ConsoleProgrammedInput<Answer>
    {
        private static readonly Answer ExitApplication = Answer.Yes;
    }
}

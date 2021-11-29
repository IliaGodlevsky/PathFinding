using ConsoleVersion.Enums;
using ConsoleVersion.ValueInput;

namespace ConsoleVersion.Interface
{
    internal interface IRequireAnswerInput
    {
        ConsoleValueInput<Answer> AnswerInput { get; set; }
    }
}

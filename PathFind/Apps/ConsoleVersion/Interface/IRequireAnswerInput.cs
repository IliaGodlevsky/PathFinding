using ConsoleVersion.Enums;

namespace ConsoleVersion.Interface
{
    internal interface IRequireAnswerInput
    {
        IInput<Answer> AnswerInput { get; set; }
    }
}

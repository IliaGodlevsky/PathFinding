using ConsoleVersion.Enums;

namespace ConsoleVersion.Interface
{
    internal interface IRequireAnswerInput
    {
        IValueInput<Answer> AnswerInput { get; set; }
    }
}

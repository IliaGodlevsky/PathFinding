using ConsoleVersion.Enums;
using ConsoleVersion.ValueInput.Interface;

namespace ConsoleVersion.ViewModel
{
    internal interface IRequireAnswerInput
    {
        IValueInput<Answer> AnswerInput { get; set; }
    }
}

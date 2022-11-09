using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console.Interface
{
    internal interface IRequireAnswerInput
    {
        IInput<Answer> AnswerInput { get; set; }
    }
}

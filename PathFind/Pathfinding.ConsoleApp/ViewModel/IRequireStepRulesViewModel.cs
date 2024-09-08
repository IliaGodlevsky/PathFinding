using Pathfinding.Service.Interface;

namespace Pathfinding.ConsoleApp.ViewModel
{
    internal interface IRequireStepRuleViewModel
    {
        (string Name, IStepRule Rule) StepRule { get; set; }
    }
}

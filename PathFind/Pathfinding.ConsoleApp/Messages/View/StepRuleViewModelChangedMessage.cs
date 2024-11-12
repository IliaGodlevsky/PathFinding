using Pathfinding.ConsoleApp.ViewModel.Interface;

namespace Pathfinding.ConsoleApp.Messages.View
{
    internal sealed record class StepRuleViewModelChangedMessage(IRequireStepRuleViewModel ViewModel);
}

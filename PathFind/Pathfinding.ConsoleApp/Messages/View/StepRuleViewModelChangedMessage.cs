using Pathfinding.ConsoleApp.ViewModel;

namespace Pathfinding.ConsoleApp.Messages.View
{
    internal sealed class StepRuleViewModelChangedMessage
    {
        public IRequireStepRuleViewModel ViewModel { get; }

        public StepRuleViewModelChangedMessage(IRequireStepRuleViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}

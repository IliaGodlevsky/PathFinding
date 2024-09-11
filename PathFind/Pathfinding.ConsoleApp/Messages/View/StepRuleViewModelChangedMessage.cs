using Pathfinding.ConsoleApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using ConsoleVersion.Interface;
using ConsoleVersion.ViewModel;
using System;
using ValueRange;
using ValueRange.Extensions;

namespace ConsoleVersion.Commands
{
    internal abstract class BaseAlgorithmSpeedCommand : IConsoleKeyCommand
    {
        private static InclusiveValueRange<int> DelayValueRange => Constants.AlgorithmDelayTimeValueRange;

        public abstract bool CanExecute(ConsoleKey key);

        public virtual void Execute(PathFindingViewModel model)
        {
            int newDelayTime = GetNewDelay(model);
            model.DelayTime = DelayValueRange.ReturnInRange(newDelayTime);
        }

        protected abstract int GetNewDelay(PathFindingViewModel viewModel);
    }
}

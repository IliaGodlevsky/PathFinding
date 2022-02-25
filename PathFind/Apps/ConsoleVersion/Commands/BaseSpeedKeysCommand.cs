using ConsoleVersion.Interface;
using ConsoleVersion.ViewModel;
using System;
using ValueRange.Extensions;

namespace ConsoleVersion.Commands
{
    internal abstract class BaseSpeedKeysCommand : IConsoleKeyCommand
    {
        public abstract bool CanExecute(ConsoleKey key);

        public virtual void Execute(PathFindingViewModel model)
        {
            int newDelayTime = GetNewDelay(model);
            model.DelayTime = Constants.AlgorithmDelayTimeValueRange.ReturnInRange(newDelayTime);
        }

        protected abstract int GetNewDelay(PathFindingViewModel viewModel);
    }
}

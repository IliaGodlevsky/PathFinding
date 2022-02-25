using Common.Attrbiutes;
using Common.Extensions;
using ConsoleVersion.ViewModel;
using System;

namespace ConsoleVersion.Commands
{
    [AttachedTo(typeof(PathFindingViewModel))]
    internal sealed class SpeedDownAlgorithmKeysCommand : BaseSpeedKeysCommand
    {
        public override bool CanExecute(ConsoleKey key)
        {
            return key.IsOneOf(ConsoleKey.DownArrow, ConsoleKey.S);
        }

        protected override int GetNewDelay(PathFindingViewModel viewModel)
        {
            return viewModel.DelayTime + 1;
        }
    }
}
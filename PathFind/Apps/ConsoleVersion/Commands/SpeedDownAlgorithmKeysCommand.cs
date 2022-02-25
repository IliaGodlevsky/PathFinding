using Common.Attrbiutes;
using Common.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.ViewModel;
using System;
using ValueRange.Extensions;

namespace ConsoleVersion.Commands
{
    [AttachedTo(typeof(PathFindingViewModel))]
    internal sealed class SpeedDownAlgorithmKeysCommand : IConsoleKeyCommand
    {
        public bool CanExecute(ConsoleKey key)
        {
            return key.IsOneOf(ConsoleKey.DownArrow, ConsoleKey.S);
        }

        public void Execute(PathFindingViewModel model)
        {
            model.DelayTime = Constants.AlgorithmDelayTimeValueRange.ReturnInRange(model.DelayTime + 1);
        }
    }
}
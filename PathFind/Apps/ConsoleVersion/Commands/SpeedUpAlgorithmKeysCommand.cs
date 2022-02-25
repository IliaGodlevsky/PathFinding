using Commands.Attributes;
using Common.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.ViewModel;
using System;
using ValueRange.Extensions;

namespace ConsoleVersion.Commands
{
    [AttachedTo(typeof(PathFindingViewModel))]
    internal sealed class SpeedUpAlgorithmKeysCommand : IConsoleKeyCommand
    {
        public bool CanExecute(ConsoleKey key)
        {
            return key.IsOneOf(ConsoleKey.UpArrow, ConsoleKey.W);
        }

        public void Execute(PathFindingViewModel model)
        {
            model.DelayTime = Constants.AlgorithmDelayTimeValueRange.ReturnInRange(model.DelayTime - 1);
        }
    }
}
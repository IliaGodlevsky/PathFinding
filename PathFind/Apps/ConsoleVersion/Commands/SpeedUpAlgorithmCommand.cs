using Common;
using Common.Attrbiutes;
using Common.Extensions;
using ConsoleVersion.ViewModel;
using System;

namespace ConsoleVersion.Commands
{
    [AttachedTo(typeof(PathFindingViewModel))]
    internal sealed class SpeedUpAlgorithmCommand : BaseAlgorithmSpeedCommand
    {
        public override bool CanExecute(ConsoleKey key)
        {
            return key.IsOneOf(ConsoleKey.UpArrow);
        }

        protected override int GetNewDelay(ValueTypeWrap<int> time)
        {
            return time.Value - 1;
        }
    }
}
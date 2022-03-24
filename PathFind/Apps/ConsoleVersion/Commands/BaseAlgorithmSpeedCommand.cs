using Common;
using ConsoleVersion.Interface;
using System;
using ValueRange;
using ValueRange.Extensions;

namespace ConsoleVersion.Commands
{
    internal abstract class BaseAlgorithmSpeedCommand : IConsoleKeyCommand<ValueTypeWrap<int>>
    {
        private static InclusiveValueRange<int> DelayValueRange => Constants.AlgorithmDelayTimeValueRange;

        public virtual void Execute(ValueTypeWrap<int> time)
        {
            int newDelayTime = GetNewDelay(time);
            time.Value = DelayValueRange.ReturnInRange(newDelayTime);
        }

        public abstract bool CanExecute(ConsoleKey key);

        protected abstract int GetNewDelay(ValueTypeWrap<int> time);
    }
}

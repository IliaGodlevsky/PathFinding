using Common.Extensions;
using ConsoleVersion.Interface;
using Random.Interface;
using System;
using ValueRange;

namespace ConsoleVersion.ValueInput.RandomInput
{
    internal abstract class RandomInput<T, TRange> : IInput<T>
        where TRange : IComparable, IComparable<TRange>
    {
        protected virtual TimeSpan Delay { get; }

        protected IRandom Random { get; }

        protected abstract InclusiveValueRange<TRange> Range { get; }

        protected RandomInput(IRandom random)
        {
            Delay = TimeSpan.FromMilliseconds(500);
            Random = random;
        }

        public virtual T Input()
        {
            T value = GetRandomValue();
            Console.WriteLine(value);
            Delay.Wait();
            return value;
        }

        protected abstract T GetRandomValue();
    }
}

using Common.Extensions;
using ConsoleVersion.Interface;
using Random.Extensions;
using Random.Interface;
using System;
using ValueRange;

namespace ConsoleVersion.ValueInput.RandomInput
{
    internal abstract class RandomInput<T> : IInput<T>
    {
        protected virtual int WaitMilliseconds => 500;

        protected IRandom Random { get; }

        protected abstract InclusiveValueRange<int> Range { get; }

        protected RandomInput(IRandom random)
        {
            Random = random;
        }

        public virtual T Input()
        {
            int value = GetRandomInt();
            T converted = ConvertFrom(value);
            Console.WriteLine(converted);
            TimeSpan.FromMilliseconds(WaitMilliseconds).Wait();
            return converted;
        }

        protected virtual int GetRandomInt()
        {
            return Random.Next(Range);
        }

        protected abstract T ConvertFrom(int value);
    }
}

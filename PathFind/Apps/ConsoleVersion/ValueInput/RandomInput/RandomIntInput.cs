using Random.Extensions;
using Random.Interface;
using ValueRange;

namespace ConsoleVersion.ValueInput.RandomInput
{
    internal sealed class RandomIntInput : RandomInput<int>
    {
        protected override InclusiveValueRange<int> Range { get; }

        public RandomIntInput(IRandom random) : base(random)
        {
            Range = new InclusiveValueRange<int>(0, 10);
        }

        protected override int ConvertFrom(int value)
        {
            return value;
        }
    }
}

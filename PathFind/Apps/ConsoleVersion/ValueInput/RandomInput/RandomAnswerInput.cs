using ConsoleVersion.Model;
using Random.Interface;
using ValueRange;

namespace ConsoleVersion.ValueInput.RandomInput
{
    internal sealed class RandomAnswerInput : RandomInput<Answer>
    {
        protected override InclusiveValueRange<int> Range { get; }

        public RandomAnswerInput(IRandom random) : base(random)
        {
            Range = new InclusiveValueRange<int>(0, 1);
        }

        protected override Answer ConvertFrom(int value)
        {
            return (Answer)value;
        }
    }
}
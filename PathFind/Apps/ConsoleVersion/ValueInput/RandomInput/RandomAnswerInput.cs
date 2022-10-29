using ConsoleVersion.Model;
using Random.Extensions;
using Random.Interface;
using ValueRange;

namespace ConsoleVersion.ValueInput.RandomInput
{
    internal sealed class RandomAnswerInput : RandomInput<Answer, int>
    {
        protected override InclusiveValueRange<int> Range { get; }

        public RandomAnswerInput(IRandom random) : base(random)
        {
            Range = new InclusiveValueRange<int>(Answer.Yes, Answer.No);
        }

        protected override Answer GetRandomValue()
        {
            return (Answer)Random.NextInt(Range);
        }
    }
}
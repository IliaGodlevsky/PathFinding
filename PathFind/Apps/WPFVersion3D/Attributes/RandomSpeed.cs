using Common.Extensions;
using Common.ValueRanges;

namespace WPFVersion3D.Attributes
{
    internal sealed class RandomSpeed : BaseSpeed
    {
        public RandomSpeed(double from, double to)
        {
            speedRange = new InclusiveValueRange<double>(from, to);
        }

        public override double Milliseconds => speedRange.GetRandomValue();

        private readonly InclusiveValueRange<double> speedRange;
    }
}
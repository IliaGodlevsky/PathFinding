using Common.Extensions;
using Common.ValueRanges;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Attributes
{
    internal sealed class RandomSpeed : BaseSpeed, IAnimationSpeed
    {
        public RandomSpeed(double from, double to)
        {
            speedRange = new InclusiveValueRange<double>(from, to);
        }

        public override double Milliseconds => speedRange.GetRandomValue();

        private readonly InclusiveValueRange<double> speedRange;
    }
}
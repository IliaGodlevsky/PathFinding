using Common.Extensions;
using Common.ValueRanges;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Attributes
{
    internal sealed class RandomAnimationSpeed : BaseAnimationSpeed, IAnimationSpeed
    {
        public RandomAnimationSpeed(double fromMilliseconds, double toMilliseconds)
        {
            speedRange = new InclusiveValueRange<double>(fromMilliseconds, toMilliseconds);
        }

        public override double Milliseconds => speedRange.GetRandomValue();

        private readonly InclusiveValueRange<double> speedRange;
    }
}
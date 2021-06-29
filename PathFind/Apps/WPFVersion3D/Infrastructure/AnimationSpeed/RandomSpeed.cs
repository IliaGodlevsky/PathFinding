using AssembleClassesLib.Attributes;
using Common.Extensions;
using Common.ValueRanges;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Infrastructure.AnimationSpeed
{
    [ClassOrder(-1)]
    [ClassName("Random speed")]
    internal sealed class RandomSpeed : IAnimationSpeed
    {
        public RandomSpeed()
        {
            var high = new VeryHighSpeed();
            var slow = new VerySlowSpeed();
            speedRange = new InclusiveValueRange<double>(slow.Milliseconds, high.Milliseconds);
        }

        public double Milliseconds => speedRange.GetRandomValue();

        private readonly InclusiveValueRange<double> speedRange;
    }
}

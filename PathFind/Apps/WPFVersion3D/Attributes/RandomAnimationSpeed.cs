using Autofac;
using Random.Extensions;
using Random.Interface;
using ValueRange;
using WPFVersion3D.DependencyInjection;

namespace WPFVersion3D.Attributes
{
    internal sealed class RandomAnimationSpeed : BaseAnimationSpeed
    {
        private readonly InclusiveValueRange<double> range;
        private readonly IRandom random;

        public override double Milliseconds => random.NextDouble(range);

        public RandomAnimationSpeed(double from, double to)
        {
            range = new InclusiveValueRange<double>(to, from);
            random = DI.Container.Resolve<IRandom>();
        }
    }
}
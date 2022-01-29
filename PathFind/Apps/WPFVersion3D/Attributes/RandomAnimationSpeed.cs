using Autofac;
using Random.Extensions;
using Random.Interface;
using ValueRange;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Attributes
{
    internal sealed class RandomAnimationSpeed : BaseAnimationSpeed, IAnimationSpeed
    {
        public RandomAnimationSpeed(int from, int to)
        {
            range = new InclusiveValueRange<int>(to, from);
            random = DI.Container.Resolve<IRandom>();
        }

        public override double Milliseconds => random.Next(range);

        private readonly InclusiveValueRange<int> range;
        private readonly IRandom random;
    }
}

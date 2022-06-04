using Autofac;
using Random.Extensions;
using Random.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ValueRange;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model
{
    internal static class AnimationSpeedsCollection
    {
        private static readonly Lazy<IReadOnlyCollection<IAnimationSpeed>> speeds;

        public static IReadOnlyCollection<IAnimationSpeed> Speeds => speeds.Value;

        static AnimationSpeedsCollection()
        {
            speeds = new Lazy<IReadOnlyCollection<IAnimationSpeed>>(GetSpeeds);
        }

        private sealed class SlowestAnimationSpeed : IAnimationSpeed
        {
            public double Milliseconds => 4800;

            public override string ToString() => "Slowest";
        }

        private sealed class SlowAnimationSpeed : IAnimationSpeed
        {
            public double Milliseconds => 2400;

            public override string ToString() => "Slow";
        }

        private sealed class MediumAnimationSpeed : IAnimationSpeed
        {
            public double Milliseconds => 1200;

            public override string ToString() => "Medium";
        }

        private sealed class HighAnimationSpeed : IAnimationSpeed
        {
            public double Milliseconds => 600;

            public override string ToString() => "High";
        }

        private sealed class HighestAnimationSpeed : IAnimationSpeed
        {
            public double Milliseconds => 300;

            public override string ToString() => "Highest";
        }

        private sealed class RandomAnimationSpeed : IAnimationSpeed
        {
            private const double From = 300;
            private const double To = 4800;

            private readonly IRandom random;
            private readonly InclusiveValueRange<double> range;

            public double Milliseconds => random.NextDouble(range);

            public RandomAnimationSpeed()
            {
                range = new InclusiveValueRange<double>(From, To);
                random = DI.Container.Resolve<IRandom>();
            }

            public override string ToString() => "Random";
        }

        private static IReadOnlyCollection<IAnimationSpeed> GetSpeeds()
        {
            var speeds = new IAnimationSpeed[]
            {
                new SlowestAnimationSpeed(),
                new SlowAnimationSpeed(),
                new MediumAnimationSpeed(),
                new HighAnimationSpeed(),
                new HighAnimationSpeed(),
                new RandomAnimationSpeed()
            };
            return new ReadOnlyCollection<IAnimationSpeed>(speeds);
        }           
    }
}

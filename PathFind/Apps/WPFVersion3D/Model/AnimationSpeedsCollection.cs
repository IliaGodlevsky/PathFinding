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

        private sealed class AnimationSpeed : IAnimationSpeed
        {
            private readonly string name;

            public double Milliseconds { get; }

            public AnimationSpeed(double milliseconds, string name)
            {
                Milliseconds = milliseconds;
                this.name = name;
            }
            
            public override string ToString() => name;
        }

        private sealed class RandomAnimationSpeed : IAnimationSpeed
        {
            private readonly IRandom random;
            private readonly InclusiveValueRange<double> range;

            public double Milliseconds => random.NextDouble(range);

            public RandomAnimationSpeed(double from, double to)
            {
                range = new InclusiveValueRange<double>(from, to);
                random = DI.Container.Resolve<IRandom>();
            }

            public override string ToString() => "Random";
        }

        private static IReadOnlyCollection<IAnimationSpeed> GetSpeeds()
        {
            var speeds = new IAnimationSpeed[]
            {
                new AnimationSpeed(4800, "Slowest"),
                new AnimationSpeed(2400, "Slow"),
                new AnimationSpeed(1200, "Medium"),
                new AnimationSpeed(600, "High"),
                new AnimationSpeed(300, "Highest"),
                new RandomAnimationSpeed(4800, 300)
            };
            return new ReadOnlyCollection<IAnimationSpeed>(speeds);
        }
    }
}

using Autofac;
using Random.Extensions;
using Random.Interface;
using System;
using System.Collections.Generic;
using ValueRange;
using WPFVersion3D.DependencyInjection;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model
{
    internal static class AnimationSpeeds
    {
        private static readonly Lazy<IReadOnlyCollection<IAnimationSpeed>> speeds;

        public static IReadOnlyCollection<IAnimationSpeed> Speeds => speeds.Value;

        static AnimationSpeeds()
        {
            speeds = new Lazy<IReadOnlyCollection<IAnimationSpeed>>(GetSpeeds);
        }

        private sealed class AnimationSpeed : IAnimationSpeed
        {
            private readonly string name;

            public TimeSpan Time { get; }

            public AnimationSpeed(TimeSpan time, string name)
            {
                Time = time;
                this.name = name;
            }

            public override string ToString() => name;
        }

        private sealed class RandomAnimationSpeed : IAnimationSpeed
        {
            private readonly IRandom random;
            private readonly InclusiveValueRange<TimeSpan> range;

            public TimeSpan Time => random.NextTimeSpan(range);

            public RandomAnimationSpeed(TimeSpan from, TimeSpan to)
            {
                range = new InclusiveValueRange<TimeSpan>(from, to);
                random = DI.Container.Resolve<IRandom>();
            }

            public override string ToString() => "Random";
        }

        public sealed class CustomAnimationSpeed : IAnimationSpeed
        {
            public TimeSpan Time { get; set; }

            public CustomAnimationSpeed(TimeSpan time)
            {
                Time = time;
            }

            public override string ToString() => "Custom";
        }

        private static IReadOnlyCollection<IAnimationSpeed> GetSpeeds()
        {           
            var speeds = new IAnimationSpeed[]
            {
                new AnimationSpeed(TimeSpan.FromSeconds(5), "Slowest"),
                new AnimationSpeed(TimeSpan.FromSeconds(2), "Slow"),
                new AnimationSpeed(TimeSpan.FromSeconds(1), "Medium"),
                new AnimationSpeed(TimeSpan.FromMilliseconds(700), "High"),
                new AnimationSpeed(TimeSpan.FromMilliseconds(400), "Highest"),
                new RandomAnimationSpeed(TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(400)),
                new CustomAnimationSpeed(TimeSpan.FromSeconds(2))
            };
            return Array.AsReadOnly(speeds);
        }
    }
}

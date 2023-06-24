using Pathfinding.App.WPF._3D.DependencyInjection;
using Pathfinding.App.WPF._3D.Interface;
using Shared.Primitives.ValueRange;
using Shared.Random;
using Shared.Random.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.WPF._3D.Model
{
    internal static class AnimationSpeeds
    {
        private static readonly Lazy<IReadOnlyCollection<IAnimationSpeed>> speeds;

        public static IEnumerable<IAnimationSpeed> Speeds => speeds.Value;

        static AnimationSpeeds()
        {
            speeds = new(() => GetSpeeds().ToArray());
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

        private static IEnumerable<IAnimationSpeed> GetSpeeds()
        {
            yield return new AnimationSpeed(TimeSpan.FromSeconds(5), "Slowest");
            yield return new AnimationSpeed(TimeSpan.FromSeconds(2), "Slow");
            yield return new AnimationSpeed(TimeSpan.FromSeconds(1), "Medium");
            yield return new AnimationSpeed(TimeSpan.FromMilliseconds(700), "High");
            yield return new AnimationSpeed(TimeSpan.FromMilliseconds(400), "Highest");
            yield return new RandomAnimationSpeed(TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(400));
            yield return new CustomAnimationSpeed(TimeSpan.FromSeconds(2));
        }
    }
}

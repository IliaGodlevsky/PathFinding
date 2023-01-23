using Pathfinding.GraphLib.Smoothing.Interface;
using Pathfinding.GraphLib.Smoothing.Localization;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Smoothing.Realizations
{
    public static class SmoothLevels
    {
        private static readonly Lazy<IReadOnlyList<ISmoothLevel>> levels;

        public static IReadOnlyList<ISmoothLevel> Levels => levels.Value;

        static SmoothLevels()
        {
            levels = new(() => GetSmoothLevels().ToReadOnly());
        }

        private sealed class SmoothLevel : ISmoothLevel
        {
            private readonly string name;

            public int Level { get; }

            public SmoothLevel(int level, string name)
            {
                Level = level;
                this.name = name;
            }

            public override string ToString() => name;
        }

        private static IEnumerable<ISmoothLevel> GetSmoothLevels()
        {
            yield return new SmoothLevel(1, Languages.Low);
            yield return new SmoothLevel(2, Languages.Medium);
            yield return new SmoothLevel(3, Languages.High);
            yield return new SmoothLevel(25, Languages.Flat);
        }
    }
}
